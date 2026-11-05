using DoNotModify;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Hit Asteroids", story: "[posPlayer] [posEnemy] [safetyMargin] si asteroids sur le chemin je tir pas", category: "Conditions", id: "5adbd5974a8a1607570b5fb350607c39")]
public partial class CheckHitAsteroidsCondition : Condition
{

    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> safetyMargin;

    public override bool IsTrue()
    {
        if (posPlayer == null || posEnemy == null)
            return false;

        float margin = safetyMargin != null ? safetyMargin.Value : 0.5f;

        List<AsteroidView> asteroids = GameManager.Instance.GetGameData().Asteroids;

        foreach (AsteroidView asteroid in asteroids)
        {
            if (IsCircleIntersectingSegment(posPlayer.Value, posEnemy.Value, asteroid.Position, asteroid.Radius + margin))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsCircleIntersectingSegment(Vector2 startPos, Vector2 endPos, Vector2 circleCenter, float circleRadius)
    {
        Vector2 segmentVector = endPos - startPos;
        Vector2 pointVector = circleCenter - startPos;

        float segmentLengthSquared = segmentVector.sqrMagnitude;

        if (segmentLengthSquared == 0)
        {
            return Vector2.Distance(startPos, circleCenter) <= circleRadius;
        }

        float t = Mathf.Max(0, Mathf.Min(1, Vector2.Dot(pointVector, segmentVector) / segmentLengthSquared));

        Vector2 closestPoint = startPos + t * segmentVector;

        return Vector2.Distance(circleCenter, closestPoint) <= circleRadius;
    }
}

