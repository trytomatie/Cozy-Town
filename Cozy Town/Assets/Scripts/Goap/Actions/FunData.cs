using CrashKonijn.Goap.Classes.References;
using UnityEngine;

public class FunData : CommonData
{
    public static readonly int DANCE = Animator.StringToHash("Dance");
    [GetComponentInChildren]
    public Animator Animator { get; set; }
    [GetComponent]
    public AgentStatsBehaviour Stats { get; set; }
}