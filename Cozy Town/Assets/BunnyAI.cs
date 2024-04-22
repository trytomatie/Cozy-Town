using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BunnyAI : MonoBehaviour
{
    public NPCStates currentState = NPCStates.Idle;
    public State[] states;
    private NavMeshAgent agent;



    public void Start()
    {
        states = new State[]
        {
            new BunnyIdle(this),
            new Wander(this)
        };
        Agent = GetComponent<NavMeshAgent>();
        ChangeState(NPCStates.Idle);
    }

    private void Update()
    {
        states[(int)currentState].Update();
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
        if (transform.position.y < -20) return;
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(path);
        }
    }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
}

public class BunnyIdle : State
{
    private float enterTime = 0f;
    private BunnyAI bunny;

    public BunnyIdle(BunnyAI bunny)
    {
        this.bunny = bunny;
    }

    public void Enter()
    {
        enterTime = Time.time;

    }

    public void Update()
    {
        if (enterTime + 5 < Time.time)
        {
            bunny.ChangeState(NPCStates.Wander);
        }
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
        // If close to destination, switch to idle and then back to wander
        if (Vector3.Distance(bunny.transform.position, destination) < 1f || enterTime + 5 < Time.time)
        {
            bunny.ChangeState(NPCStates.Idle);
        }
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
    Follow,
    Attack,
    Dead
}
