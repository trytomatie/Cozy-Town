using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;


public class DanceTargetSensor : LocalTargetSensorBase, IInjectable
{
    public FunConfig_SO config;
    private Collider[] colliders = new Collider[30];

    public override void Created()
    {

    }

    public override void Update()
    {

    }

    public override ITarget Sense(IMonoAgent agent, IComponentReference references)
    {
        int hits = Physics.OverlapSphereNonAlloc(agent.transform.position, config.sensorRadius, colliders, config.sensorMask);
        if (hits > 0)
        {
            for(int i = colliders.Length-1; i > hits; i--)
            {
                colliders[i] = null;
            }
            colliders = colliders.Where(e => e != null).OrderBy(e => Vector3.Distance(agent.transform.position,e.transform.position)).ToArray();
            foreach(Collider col in colliders)
            {
                if (col == null) break;
                if(col.gameObject.CompareTag("DanceSpot"))
                {
                    return new TransformTarget(col.transform);
                }
            }
        }
        return null;
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

    public void Inject(DependencyInjector injector)
    {
        config = injector.funConfig;
    }
}
