using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMoveBehaviour : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    public Animator animator;
    private AgentBehaviour agentBehaviour;
    private ITarget currentTarget;
    private Vector3 lastPosition;
    private static readonly int SPEED = Animator.StringToHash("Speed");
    
     
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        agentBehaviour = GetComponent<AgentBehaviour>();
        navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        animator.SetFloat(SPEED, navMeshAgent.velocity.magnitude);
        if (currentTarget != null)
        {
            return;
        }
        if(10 <= Vector3.Distance(lastPosition, transform.position))
        {
            lastPosition = currentTarget.Position;
            navMeshAgent.SetDestination(currentTarget.Position);
        }
    }

    private void LateUpdate()
    {
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = new Quaternion(transform.rotation.x,Quaternion.LookRotation(navMeshAgent.velocity.normalized).y,transform.rotation.z,transform.rotation.w);
        }
    }

    private void OnEnable()
    {
        agentBehaviour.Events.OnTargetInRange += EventsOnTargetInRange;
        agentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
        agentBehaviour.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;

    }

    private void OnDisable()
    {
        agentBehaviour.Events.OnTargetInRange -= EventsOnTargetInRange;
        agentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
        agentBehaviour.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;
    }

    private void EventsOnTargetChanged(ITarget target, bool inRange)
    {
        lastPosition = target.Position;
        currentTarget = target;
        navMeshAgent.SetDestination(target.Position);
    }

    private void EventsOnTargetOutOfRange(ITarget target)
    {

    }

    private void EventsOnTargetInRange(ITarget target)
    {
        currentTarget = target;
    }
}
