using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class DanceTargetSensor : LocalTargetSensorBase, IInjectable
{
    public FunConfig_SO config;
    public override void Created()
    {

    }

    public override void Update()
    {

    }

    public override ITarget Sense(IMonoAgent agent, IComponentReference references)
    {
        
        InteractionObject io = InteractionCollection.Instance.Get<DanceSpot_InteractionObject>().OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position)).Where(e => e.occupant == null).FirstOrDefault();
        if(io != null)
        {
            return new TransformTarget(io.transform);
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
