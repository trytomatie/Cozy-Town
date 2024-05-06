using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class EnergySensor : LocalWorldSensorBase
{

    public override void Created()
    {

    }

    public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
    {
        return new SenseValue(Mathf.CeilToInt(references.GetCachedComponent<AgentStatsBehaviour>().Energy));
    }



    public override void Update()
    {

    }
}
