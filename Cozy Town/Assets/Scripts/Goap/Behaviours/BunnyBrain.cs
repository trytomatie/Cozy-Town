using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(AgentBehaviour))]
public class BunnyBrain : MonoBehaviour
{
    private AgentBehaviour agentBehaviour;
    private AgentStatsBehaviour stats;
    private void Awake()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
        stats = GetComponent<AgentStatsBehaviour>();
    
    }

    private void Start()
    {
        agentBehaviour.SetGoal<WanderGoal>(false);
    }

    private void OnEnable()
    {
        agentBehaviour.Events.OnNoActionFound += EventsOnNoActionFound;
    }


    private void OnDisable()
    {
        agentBehaviour.Events.OnNoActionFound -= EventsOnNoActionFound;
    }

    private void Update()
    {
        if(stats.Fun < 75 && InteractionCollection.Instance.Get<Fun_InteractionObject>().Where(e => e.Occupant == null).Count() > 0)
        {
            agentBehaviour.SetGoal<HaveFunGoal>(true);
            return;
        }
        if(stats.Energy < 20 && InteractionCollection.Instance.Get<RestingPlace_InteractionObject>().Where(e => e.Occupant == null).Count() > 0)
        {
            agentBehaviour.SetGoal<RestoreEnergyGoal>(true);
            return;
        }

        agentBehaviour.SetGoal<WanderGoal>(false);

    }

    private void EventsOnNoActionFound(IGoalBase goal)
    {
        agentBehaviour.SetGoal<WanderGoal>(false);
    }

}
