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
    public float energy = 100; public float Energy { get => energy; set => energy = value; }
    public bool HavingFun { get; set; }
    public bool Resting { get; set; }

    public float social = 100; public float Social { get => social; set => social = value; }

    private void Start()
    {
        Fun = UnityEngine.Random.Range(25,100);
        Energy = UnityEngine.Random.Range(25, 100);
        Social = UnityEngine.Random.Range(25, 100);
    }
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
