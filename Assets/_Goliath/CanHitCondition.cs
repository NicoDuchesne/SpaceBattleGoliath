using DoNotModify;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanHit", story: "Peut toucher si [posPlayer] vise [posEnemy] selon [playerOrientation] et [angleTolerance]", category: "Conditions", id: "9273bf9765957ee45070356bdce1b1a7")]
public partial class CanHitCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> playerOrientation;
    [SerializeReference] public BlackboardVariable<float> angleTolerance;

    public override bool IsTrue()
    {
        if (posPlayer == null || posEnemy == null || playerOrientation == null)
        {
            return false;
        }

        Vector2 directionVersEnnemi = (posEnemy.Value - posPlayer.Value).normalized;

        float orientationAngleRad = (playerOrientation.Value) * Mathf.Deg2Rad;
        Vector2 directionVaisseau = new Vector2(Mathf.Cos(orientationAngleRad), Mathf.Sin(orientationAngleRad));

        float angleDiff = Vector2.Angle(directionVaisseau, directionVersEnnemi);
        bool canHitResult = angleDiff <= angleTolerance.Value;

        return angleDiff <= angleTolerance.Value;
    }
}
