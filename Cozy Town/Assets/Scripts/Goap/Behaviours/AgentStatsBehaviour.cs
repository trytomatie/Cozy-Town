using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentStatsBehaviour : MonoBehaviour
{
    [SerializeField] public float fun = 100; public float Fun{ get => fun; private set => fun = value; }
    public float Energy { get; private set; }
    public bool HavingFun { get; set; }
    public bool Resting { get; set; }
    public int DanceSpots { get => InteractionObject.interactionDictionary.Values.Where(e => e == InteractionType.DanceSpot).Count(); }

    private void Update()
    {
        if(!HavingFun) Fun -= Time.deltaTime; ;

        if(!Resting) Energy -= Time.deltaTime;
    }

    public void IncreaseFun(float amount)
    {
        Fun += amount;
    }
}
