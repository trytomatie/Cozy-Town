using CrashKonijn.Goap.Behaviours;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AgentBehaviour))]
public class BunnyBrain : MonoBehaviour
{
    private AgentBehaviour agentBehaviour;

    private void Awake()
    {
        agentBehaviour = GetComponent<AgentBehaviour>();
    
    }

    private void Start()
    {
        agentBehaviour.SetGoal<WanderGoal>(false);
    }
}
