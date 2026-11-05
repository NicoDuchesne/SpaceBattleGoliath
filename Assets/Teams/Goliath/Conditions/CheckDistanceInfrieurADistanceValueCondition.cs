using System;
using Unity.Behavior;
using UnityEngine;

namespace GoliathTeam
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Check Distance inférieur a distanceValue", story: "Check Distance Entre [posPlayer] et [posEnemy] < [distanceValue]", category: "Conditions", id: "6d673fbd6c8cca15a37f1faa6bb25b2e")]
    public partial class CheckDistanceInfrieurADistanceValueCondition : Condition
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

            return Vector2.Distance(posPlayer.Value, posEnemy.Value) < distanceValue.Value;
        }
    }
}