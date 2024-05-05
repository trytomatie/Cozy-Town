using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using System;
using UnityEngine;
using UnityEngine.AI;

public class WanderTargetSensor : LocalTargetSensorBase
{
    public override void Created()
    {

    }

    public override void Update()
    {

    }

    public override ITarget Sense(IMonoAgent agent, IComponentReference references)
    {
        Vector3 position = GetRandomPosition(agent);
        return new PositionTarget(position);
    }

    private Vector3 GetRandomPosition(IMonoAgent agent)
    {
        int i = 0;

        while (i < 5)
        {
            Vector2 rnd = UnityEngine.Random.insideUnitCircle * 10;
            Vector3 pos = agent.transform.position + new Vector3(rnd.x, 0, rnd.y);
            if(NavMesh.SamplePosition(pos, out NavMeshHit hit, 10, NavMesh.AllAreas))
            {
                return hit.position;
            }
            i++;
        }
        return agent.transform.position;
    }


}
