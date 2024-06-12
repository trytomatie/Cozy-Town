using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BunnyAI : MonoBehaviour
{
    public NPCStates currentState = NPCStates.Idle;
    public State[] states;
    private NavMeshAgent agent;
    public InteractionObject currentInteraction;
    public Animator anim;
    public GameObject[] leftHandEquipment;
    public GameObject[] rightHandEquipment;
    public Vector3 debug_CurrentDestination;

    public float fun = 100;
    public float stamina = 100;

    public void Start()
    {
        states = new State[]
        {
            new BunnyIdle(this),
            new Wander(this),
            new GoToFun(this),
            new HaveFun(this),
            new GoToReplenishStamina(this),
            new RegenerateStamina(this)
        };
        Agent = GetComponent<NavMeshAgent>();
        ChangeState(NPCStates.Idle);
        Equip(0);
    }

    private void Update()
    {
        states[(int)currentState].Update();
        fun -= Time.deltaTime;
        stamina -= Time.deltaTime;
        
    }

    public void Equip(int i)
    {
        foreach (var item in leftHandEquipment)
        {
            item.SetActive(false);
        }
        if (i == 0)
        {
            return;
        }
        i -= 1;
        if (i < leftHandEquipment.Length)
        {
            leftHandEquipment[i].SetActive(true);
        }
        if (i < rightHandEquipment.Length)
        {
            rightHandEquipment[i].SetActive(true);
        }
    }

    public void AnimateMovement()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
    public void AnimateMovement(int speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void Interact(InteractionObject io)
    {
        transform.position = io.transform.position;
        transform.rotation = io.transform.rotation;
        agent.transform.position = io.transform.position;
        agent.transform.rotation = io.transform.rotation;
        anim.SetInteger("Interaction", (int)io.animationType);
    }

    public void ChangeState(NPCStates targetState)
    {
        if (states[(int)currentState] != null)
        {
            states[(int)currentState].Exit();
        }
        currentState = targetState;
        states[(int)currentState].Enter();
    }
    public void SetDestinaton(Vector3 destination)
    {
        debug_CurrentDestination = destination;
        if (transform.position.y < -20) return;
        NavMeshPath path = new NavMeshPath();
        NavMesh.SamplePosition(destination, out NavMeshHit hit, 100, NavMesh.AllAreas);
        agent.CalculatePath(hit.position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(path);
        }
        else
        {
            Debug.LogError("Path not found");
        }
    }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
}

public class BunnyIdle : State
{
    private float enterTime = 0f;
    private BunnyAI bunny;
    private float randomWanderTime;

    public BunnyIdle(BunnyAI bunny)
    {
        this.bunny = bunny;
    }

    public void Enter()
    {
        enterTime = Time.time;
        if(bunny.fun < 75 && InteractionCollection.CountOfAvailableFunSpots() > 0)
        {
            bunny.ChangeState(NPCStates.GoToFun);
            return;
        }
        if(bunny.stamina < 75 && InteractionCollection.CountOfAvailableRestingSpots() > 0)
        {
            bunny.ChangeState(NPCStates.GoToReplenishStamina);
            return;
        }
        randomWanderTime = Random.Range(1, 5);
    }

    public void Update()
    {
        if(bunny.currentState != NPCStates.Idle)
        {
            return;
        }
        if (enterTime + randomWanderTime < Time.time)
        {
            bunny.ChangeState(NPCStates.Wander);
        }
        bunny.AnimateMovement();
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}

public class Wander : State
{
    private BunnyAI bunny;
    private Vector3 destination;
    private float wanderRadius = 15f;
    private float enterTime = 0f;

    public Wander(BunnyAI pc)
    {
        bunny = pc;
    }
    public void Enter()
    {
        enterTime = Time.time;
        // Generate random point in wander radius
        int sampleCount = 0;
        NavMeshHit hit = new NavMeshHit();
        while (sampleCount < 5)
        {
            destination = bunny.transform.position + Random.insideUnitSphere * wanderRadius;
            if (!NavMesh.SamplePosition(destination, out hit, wanderRadius, bunny.Agent.areaMask))
            {
                sampleCount++;
            }
            else
            {
                break;
            }

        }

        destination = hit.position;
        bunny.SetDestinaton(destination);

    }

    public void Exit()
    {

    }

    public void Update()
    {
        bunny.AnimateMovement();
        // If close to destination, switch to idle and then back to wander
        if ((Vector3.Distance(bunny.transform.position, destination) < 1f && enterTime + 5 < Time.time) || enterTime + 20 < Time.time)
        {
            bunny.ChangeState(NPCStates.Idle);
        }
    }
}

public class GoToFun : State
{
    public BunnyAI agent;
    public InteractionObject io;
    public GoToFun(BunnyAI pc)
    {
        agent = pc;
    }
    public void Enter()
    {
        // Find Closest Fun Object
        io = InteractionCollection.Instance.Get<Fun_InteractionObject>().OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position)).Where(e => e.Occupant == null).FirstOrDefault();
        if(io == null)
        {
            agent.ChangeState(NPCStates.Wander);
            return;
        }
        io.Occupant = agent.gameObject;
        // Set Destination
        agent.SetDestinaton(io.transform.position);


    }
    public void Update()
    {
        agent.AnimateMovement();
        if (Vector3.Distance(agent.transform.position, io.transform.position) < 1f)
        {
            agent.currentInteraction = io;
            agent.ChangeState(NPCStates.HaveFun);
        }
    }
    public void Exit()
    {

    }
}

public class HaveFun : State
{
    public BunnyAI agent;
    public InteractionObject io;
    private float exitTime = 5;
    private float enterTime;
    public HaveFun(BunnyAI pc)
    {
        agent = pc;
    }
    public void Enter()
    {
        agent.AnimateMovement(0);
        io = agent.currentInteraction;
        agent.Interact(io);
        enterTime = Time.time;
        exitTime = enterTime + io.duration;
        agent.Agent.enabled = false;
        agent.Equip((int)io.equipedItem);
    }


    public void Update()
    {
        if (Time.time > exitTime)
        {
            agent.ChangeState(NPCStates.Wander);
        }
    }
    public void Exit()
    {
        agent.Agent.enabled = true; ;
        agent.fun += 50;
        io.Occupant = null;
        agent.anim.SetInteger("Interaction", 0);
    }
}

public class GoToReplenishStamina : State
{
    public BunnyAI agent;
    public InteractionObject io;
    public GoToReplenishStamina(BunnyAI pc)
    {
        agent = pc;
    }
    public void Enter()
    {
        io = InteractionCollection.Instance.Get<RestingPlace_InteractionObject>().OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position)).Where(e => e.Occupant == null).FirstOrDefault();
        if (io == null)
        {
            agent.ChangeState(NPCStates.Wander);
            return;
        }
        io.Occupant = agent.gameObject;
        // Set Destination
        agent.SetDestinaton(io.transform.position);
    }
    public void Update()
    {
        agent.AnimateMovement();
        if (Vector3.Distance(agent.transform.position, io.transform.position) < 1f)
        {
            agent.currentInteraction = io;
            agent.ChangeState(NPCStates.RegenerateStamina);
        }
    }
    public void Exit()
    {

    }
}

public class RegenerateStamina : State
{
    public BunnyAI agent;
    public InteractionObject io;
    private float exitTime = 5;
    private float enterTime;
    public RegenerateStamina(BunnyAI pc)
    {
        agent = pc;
    }
    public void Enter()
    {
        agent.AnimateMovement(0);
        io = agent.currentInteraction;
        agent.Interact(io);
        enterTime = Time.time;
        exitTime = enterTime + io.duration;
        agent.Agent.enabled = false;
        agent.Equip((int)io.equipedItem);
    }


    public void Update()
    {
        if (Time.time > exitTime)
        {
            agent.ChangeState(NPCStates.Wander);
        }
    }
    public void Exit()
    {
        agent.Agent.enabled = true;
        agent.stamina += 50;
        io.Occupant = null;
        agent.anim.SetInteger("Interaction", 0);
    }
}

public interface State
{
    public void Enter();
    public void Update();
    public void Exit();
}

public enum NPCStates
{
    Idle,
    Wander,
    GoToFun,
    HaveFun,
    GoToReplenishStamina,
    RegenerateStamina,
    Dead
}
