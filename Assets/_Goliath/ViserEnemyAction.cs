using DoNotModify;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEditor.Build;
using UnityEngine;
using Action = Unity.Behavior.Action;

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
        // On vérifie que enemyVelocity est bien présent
        if (posEnemy == null || posPlayer == null || thrust == null || targetOrient == null || enemyVelocity == null)
        {
            return Status.Failure;
        }

        thrust.Value = 0.0f;

        // 1. Sécurité pour le premier frame d'initialisation
        if (posPlayer.Value == posEnemy.Value)
        {
            return Status.Running;
        }

        // 2. Calcul du temps de vol de la balle
        float distance = Vector2.Distance(posPlayer.Value, posEnemy.Value);
        // On récupère la vitesse de la balle (utilisée dans ton AimingHelpers)
        float timeToReach = distance / Bullet.Speed;

        // 3. Calcul de la position FUTURE de l'ennemi (Pre-shot)
        // Position actuelle + (Vecteur vitesse * Temps)
        Vector2 futureEnemyPos = posEnemy.Value + (enemyVelocity.Value * timeToReach);

        // 4. On calcule le vecteur de direction vers cette position anticipée
        Vector2 aimDirection = (futureEnemyPos - posPlayer.Value).normalized;

        // 5. On applique l'angle sans offset (comme on l'a validé, le modèle est bien orienté pour la visée)
        targetOrient.Value = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        return Status.Success;
    }

    //protected override Status OnUpdate()
    //{
    //    if (posEnemy == null || posPlayer == null || thrust == null || targetOrient == null)
    //    {
    //        Debug.LogError($"[ViserEnemyAction] Une variable est manquante ! posPlayer présent: {posPlayer != null}, posEnemy présent: {posEnemy != null}");
    //        return Status.Failure;
    //    }

    //    thrust.Value = 0.0f;

    //    Vector2 direction = (posEnemy.Value - posPlayer.Value).normalized;

    //    if (direction == Vector2.zero)
    //    {
    //        return Status.Running;
    //    }

    //    targetOrient.Value = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    return Status.Success;
    //}
}

