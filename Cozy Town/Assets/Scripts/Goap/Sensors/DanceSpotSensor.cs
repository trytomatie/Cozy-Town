using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using System.Linq;
using UnityEngine;

public class DanceSpotSensor : LocalWorldSensorBase
{
    private Collider[] colliders = new Collider[30];

    public override void Created()
    {

    }

    public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
    {
        return new SenseValue(InteractionCollection.Instance.Get<Fun_InteractionObject>().Count());
    }



    public override void Update()
    {

    }
}
