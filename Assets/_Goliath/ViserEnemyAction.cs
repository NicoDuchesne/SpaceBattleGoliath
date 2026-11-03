using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Viser Enemy", story: "Viser [posEnemy] depuis [posPlayer] avec [thrust] et [targetOrient]", category: "Action", id: "9ce164da1972ebf043dabd055be53a47")]
public partial class ViserEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> thrust;
    [SerializeReference] public BlackboardVariable<float> targetOrient;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (posPlayer == null || posEnemy == null || thrust == null || targetOrient == null)
        {
            return Status.Failure;
        }

        thrust.Value = 0.0f;

        Vector2 direction = (posEnemy.Value - posPlayer.Value).normalized;
        targetOrient.Value = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

