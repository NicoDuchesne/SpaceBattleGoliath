using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Distance Entre posPlayer et posEnemy", story: "[posPlayer] [posEnemy] [needShoot] [triggerShockwave] [distanceValue]", category: "Flow", id: "9a08330c44e4e40cf49bfdfaf6416a82")]
public partial class DistanceEntrePosPlayerEtEnemyModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<Vector2> PosPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> PosEnemy;
    [SerializeReference] public BlackboardVariable<bool> NeedShoot;
    [SerializeReference] public BlackboardVariable<bool> TriggerShockwave;
    [SerializeReference] public BlackboardVariable<float> DistanceValue;

    protected override Status OnStart()
    {
        TriggerShockwave.Value = false;
        NeedShoot.Value = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Vector2.Distance(PosPlayer.Value, PosEnemy.Value) < DistanceValue)
        {
            NeedShoot.Value = false;
            TriggerShockwave.Value = true;
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }
    protected override void OnEnd()
    {
    }
}

