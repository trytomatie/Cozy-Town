using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using System;
using System.Collections;
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
        if(stats.Fun < 75 && stats.DanceSpots > 0)
        {
            agentBehaviour.SetGoal<HaveFunGoal>(true);
        }
        else
        {
            agentBehaviour.SetGoal<WanderGoal>(true);
        }
    }

    private void EventsOnNoActionFound(IGoalBase goal)
    {
        agentBehaviour.SetGoal<WanderGoal>(true);
    }

}
