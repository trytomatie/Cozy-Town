using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Runners;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AgentBehaviour))]
public class GoapSetBinder : MonoBehaviour
{
    public GoapRunnerBehaviour goapRunnerBehaviour;

    private void Awake()
    {
        AgentBehaviour agent = GetComponent<AgentBehaviour>();
        agent.GoapSet = goapRunnerBehaviour.GetGoapSet("BunnySet");
    
    }
}
