using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Suivre Enemy ", story: "Suis [posEnemy] de mon [posPlayer] avec [thrust] et [targetOrient]", category: "Action", id: "b414503689bcb14d25a6ee99e6382aa1")]
public partial class SuivreEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> thrust;
    [SerializeReference] public BlackboardVariable<float> targetOrient;

    protected override Status OnStart(){
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (posPlayer == null || posEnemy == null || thrust == null || targetOrient == null)
        {
            return Status.Failure;
        }

        thrust.Value = 1.0f;

        Vector2 direction = (posEnemy.Value - posPlayer.Value).normalized;
        targetOrient.Value = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        return Status.Success;
    }
}

