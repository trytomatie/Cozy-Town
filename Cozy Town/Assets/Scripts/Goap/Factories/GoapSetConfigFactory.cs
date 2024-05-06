using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Resolver;
using System;
using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(DependencyInjector))]
public class GoapSetConfigFactory : GoapSetFactoryBase
{
    private DependencyInjector DependencyInjector { get; set; }
    public override IGoapSetConfig Create()
    {
        DependencyInjector = GetComponent<DependencyInjector>();
        GoapSetBuilder builder = new GoapSetBuilder("BunnySet");

        BuildGoals(builder);
        BuildActions(builder);
        BuildSensors(builder);

        return builder.Build();
    }

    private void BuildGoals(GoapSetBuilder builder)
    {
        builder.AddGoal<WanderGoal>()
            .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);

        builder.AddGoal<HaveFunGoal>()
            .AddCondition<Fun>(Comparison.GreaterThanOrEqual, 75);

        builder.AddGoal<RestoreEnergyGoal>()
            .AddCondition<Energy>(Comparison.GreaterThanOrEqual, 50);
    }

    private void BuildActions(GoapSetBuilder builder)
    {
        builder.AddAction<WanderAction>()
           .SetTarget<WanderTarget>()
           .AddEffect<IsWandering>(EffectType.Increase)
           .SetBaseCost(5)
           .SetInRange(20);

        builder.AddAction<DanceAction>()
            .SetTarget<DanceTarget>()
            .AddEffect<Fun>(EffectType.Increase)
            .SetBaseCost(8)
            .SetInRange(1);

        builder.AddAction<ReplenishEnergyAction>()
            .SetTarget<RestingTarget>()
            .AddEffect<Energy>(EffectType.Increase)
            .SetBaseCost(4)
            .SetInRange(1);
    }

    private void BuildSensors(GoapSetBuilder builder)
    {
        builder.AddTargetSensor<WanderTargetSensor>()
            .SetTarget<WanderTarget>();

        builder.AddTargetSensor<DanceTargetSensor>()
            .SetTarget<DanceTarget>();

        builder.AddWorldSensor<FunSensor>()
            .SetKey<Fun>();

        builder.AddWorldSensor<DanceSpotSensor>()
            .SetKey<DanceSpot>();

        builder.AddWorldSensor<EnergySensor>()
            .SetKey<Energy>();

        builder.AddTargetSensor<RestingTargetSensor>()
            .SetTarget<RestingTarget>();
    }
}
