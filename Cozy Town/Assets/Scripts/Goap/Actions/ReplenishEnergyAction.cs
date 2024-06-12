using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class ReplenishEnergyAction : ActionBase<InteractionData>
{
    public override void Created()
    {
    }
    public override void Start(IMonoAgent agent, InteractionData data)
    {
        if(data.Target == null)
        {
            data.ActionStarted = true;
            data.Timer = 0;
            return;
        }
        data.ActionStarted = false;
        data.interactionTarget = (data.Target as TransformTarget).Transform.GetComponent<InteractionObject>();
        if(data.interactionTarget == null)
        {
            return;
        }
        data.interactionTarget.Occupant = agent.gameObject;
    }

    public override ActionRunState Perform(IMonoAgent agent, InteractionData data, ActionContext context)
    {
        if (data.interactionTarget == null || data.interactionTarget.Occupant != agent.gameObject)
        {
            return ActionRunState.Stop;
        }
        if (!data.ActionStarted)
        {
            if (data.agentBehaviour.MoveState == AgentMoveState.InRange)
            {
                data.Stats.Resting = true;
                data.Animator.SetInteger("Interaction", (int)data.interactionTarget.animationType);
                agent.transform.position = data.Target.Position;
                agent.transform.rotation = data.TargetTransform.Transform.rotation;
                agent.GetComponent<BunnyBrain>().Equip((int)data.interactionTarget.equipedItem);
                data.interactionTarget.interacting = true;
                data.Stats.GetComponent<NavMeshAgent>().enabled = false;
                data.Timer = Random.Range(5, 20);
                data.ActionStarted = true;
            }
            return ActionRunState.Continue;
        }
        else
        {
            agent.transform.position = data.Target.Position;
            agent.transform.rotation = data.TargetTransform.Transform.rotation;
            data.Timer -= context.DeltaTime;
            if (data.Timer > 0)
            {

                return ActionRunState.Continue;
            }
        }
        return ActionRunState.Stop;

    }

    public override void End(IMonoAgent agent, InteractionData data)
    {
        data.Stats.GetComponent<NavMeshAgent>().enabled = true;
        data.Animator.SetInteger("Interaction", 0);
        if(data.Stats.Resting) data.Stats.energy += 60;
        data.Stats.Resting = false;
        if(data.interactionTarget != null) data.interactionTarget.Occupant = null;

    }


}
