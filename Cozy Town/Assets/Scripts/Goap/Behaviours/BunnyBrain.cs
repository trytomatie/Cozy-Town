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
    public GameObject[] leftHandEquipment;
    public GameObject[] rightHandEquipment;
    private void Awake()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
        stats = GetComponent<AgentStatsBehaviour>();
        foreach (var item in leftHandEquipment)
        {
            item.SetActive(false);
        }
    }

    public void Equip(int i)
    {
        foreach (var item in leftHandEquipment)
        {
            item.SetActive(false);
        }
        if(i == 0)
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
        print(InteractionCollection.CountOfAvailableFunSpots());
        if(stats.Fun < 75 && InteractionCollection.CountOfAvailableFunSpots() > 0)
        {
            agentBehaviour.SetGoal<HaveFunGoal>(true);
            return;
        }
        if(stats.Energy < 20 && InteractionCollection.CountOfAvailableFunSpots() > 0)
        {
            agentBehaviour.SetGoal<RestoreEnergyGoal>(true);
            return;
        }
        agentBehaviour.SetGoal<WanderGoal>(true);

    }

    private void EventsOnNoActionFound(IGoalBase goal)
    {
        agentBehaviour.SetGoal<WanderGoal>(false);
    }

}
