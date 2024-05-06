using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using UnityEngine;

public class DanceAction : ActionBase<FunData>
{

    public override void Created()
    {
        
    }
    public override void Start(IMonoAgent agent, FunData data)
    {
        data.Stats.HavingFun = true;
        data.Animator.SetBool(FunData.DANCE, true);
        agent.transform.position = data.Target.Position;
        data.Timer = Random.Range(15, 15);
    }



    public override ActionRunState Perform(IMonoAgent agent, FunData data, ActionContext context)
    {
        data.Timer -= context.DeltaTime;
        if(data.Timer > 0)
        {

            return ActionRunState.Continue;
        }
        return ActionRunState.Stop;

    }

    public override void End(IMonoAgent agent, FunData data)
    {
        data.Animator.SetBool(FunData.DANCE, false);
        data.Stats.HavingFun = false;
        data.Stats.IncreaseFun(40);
    }


}
