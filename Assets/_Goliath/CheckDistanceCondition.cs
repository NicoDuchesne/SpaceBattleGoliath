using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Distance", story: "Check Distance Entre [posPlayer] et [posEnemy] par rapport a la [distanceValue]", category: "Conditions", id: "c9b827c4411eb3b62418c32c6ae873fe")]
public partial class CheckDistanceCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> distanceValue;

    public override bool IsTrue()
    {
        if (posPlayer == null || posEnemy == null || distanceValue == null)
        {
            return false;
        }

        return Vector2.Distance(posPlayer.Value, posEnemy.Value) > distanceValue.Value;
    }
}
