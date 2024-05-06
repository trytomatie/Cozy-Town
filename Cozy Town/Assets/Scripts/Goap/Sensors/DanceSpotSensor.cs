using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class DanceSpotSensor : LocalWorldSensorBase
{
    private Collider[] colliders = new Collider[30];

    public override void Created()
    {

    }

    public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
    {
        return new SenseValue(InteractionCollection.Instance.Get<DanceSpot_InteractionObject>().Count());
    }



    public override void Update()
    {

    }
}
