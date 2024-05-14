using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using UnityEngine;

public class FunSensor : LocalWorldSensorBase
{

    public override void Created()
    {

    }

    public override SenseValue Sense(IMonoAgent agent, IComponentReference references)
    {
        return new SenseValue(Mathf.CeilToInt(references.GetCachedComponent<AgentStatsBehaviour>().Fun));
    }



    public override void Update()
    {

    }
}
