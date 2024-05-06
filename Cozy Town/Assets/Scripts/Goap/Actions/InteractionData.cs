using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.References;
using UnityEngine;

public class InteractionData : CommonData
{

    [GetComponentInChildren]
    public Animator Animator { get; set; }
    [GetComponent]
    public AgentStatsBehaviour Stats { get; set; }
    [GetComponent]
    public AgentBehaviour agentBehaviour { get; set; }
}