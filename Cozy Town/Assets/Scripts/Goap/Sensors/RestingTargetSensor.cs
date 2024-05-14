using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using System.Linq;
using UnityEngine;
public class RestingTargetSensor : LocalTargetSensorBase
{

    public override void Created()
    {

    }

    public override ITarget Sense(IMonoAgent agent, IComponentReference references)
    {
        InteractionObject io = InteractionCollection.Instance.Get<RestingPlace_InteractionObject>().OrderBy(x => Vector3.Distance(x.transform.position, agent.transform.position)).Where(e => e.occupant == null).FirstOrDefault();
        if (io != null)
        {
            return new TransformTarget(io.transform);
        }
        return null;
    }



    public override void Update()
    {

    }
}
