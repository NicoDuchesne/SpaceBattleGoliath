using DoNotModify;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CanHit", story: "[playerVaisseau] : Déterminer si une cible [posEnemy] en mouvement sera touché par un tir du vaisseau [posPlayer] avec un intervalle de temps de tolérance donné [hitTimeTolerance] [enemyVelocity].", category: "Conditions", id: "9273bf9765957ee45070356bdce1b1a7")]
public partial class CanHitCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Vector2> posPlayer;
    [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
    [SerializeReference] public BlackboardVariable<float> playerOrientation;
    [SerializeReference] public BlackboardVariable<float> hitTimeTolerance;
    [SerializeReference] public BlackboardVariable<SpaceShip> playerVaisseau;
    [SerializeReference] public BlackboardVariable<float> angleTolerance;
    [SerializeReference] public BlackboardVariable<Vector2> enemyVelocity;
    
    public override bool IsTrue()
    {
        if (posPlayer == null || posEnemy == null || playerOrientation == null || hitTimeTolerance == null || enemyVelocity == null || playerVaisseau == null)
        {
            return false;
        }

        return AimingHelpers.CanHit(playerVaisseau.Value.view, posEnemy.Value, enemyVelocity.Value, hitTimeTolerance.Value);
    }
}
