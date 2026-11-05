using DoNotModify;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor.Build;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Viser Enemy", story: "[enemyVelocity] [posPlayer] Viser[posEnemy] depuis[playerVaisseau] [overshootFactor] et [targetOrient] et avec[thrust]", category: "Action", id: "9ce164da1972ebf043dabd055be53a47")]
    public partial class ViserEnemyAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> posEnemy;
        [SerializeReference] public BlackboardVariable<Vector2> posPlayer;

        [SerializeReference] public BlackboardVariable<float> thrust;
        [SerializeReference] public BlackboardVariable<float> targetOrient;
        [SerializeReference] public BlackboardVariable<float> overshootFactor;
        [SerializeReference] public BlackboardVariable<SpaceShip> playerVaisseau;

        [SerializeReference] public BlackboardVariable<Vector2> enemyVelocity;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (posEnemy == null || posPlayer == null || thrust == null || targetOrient == null || enemyVelocity == null)
            {
                return Status.Failure;
            }

            thrust.Value = 0.0f;

            if (posPlayer.Value == posEnemy.Value)
            {
                return Status.Running;
            }

            float distance = Vector2.Distance(posPlayer.Value, posEnemy.Value);

            float timeToReach = distance / Bullet.Speed;

            Vector2 futureEnemyPos = posEnemy.Value + (enemyVelocity.Value * timeToReach);

            Vector2 aimDirection = (futureEnemyPos - posPlayer.Value).normalized;

            targetOrient.Value = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            return Status.Success;
        }

    }
}

