using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

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
