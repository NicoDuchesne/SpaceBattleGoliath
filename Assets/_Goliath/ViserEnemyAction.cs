using DoNotModify;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Viser Enemy", story: "Viser[posEnemy] depuis[playerVaisseau] [overshootFactor] et [targetOrient] et avec[thrust]", category: "Action", id: "9ce164da1972ebf043dabd055be53a47")]
public partial class ViserEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> thrust;
    [SerializeReference] public BlackboardVariable<float> targetOrient;
    [SerializeReference] public BlackboardVariable<float> overshootFactor;
    [SerializeReference] public BlackboardVariable<SpaceShip> playerVaisseau;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (posEnemy == null || thrust == null || targetOrient == null || overshootFactor == null || playerVaisseau == null || playerVaisseau.Value == null)
        {
            return Status.Failure;
        }

        thrust.Value = 0.0f;

        float shootValue = AimingHelpers.ComputeSteeringOrient(playerVaisseau.Value.view, posEnemy.Value, overshootFactor.Value);
        targetOrient.Value = shootValue + 180f;
        return Status.Success;
    }
}

