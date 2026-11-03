using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Enemy IsHit", story: "Check si enemy n'est ni ralenti [enemyHitPenaltyCountdown] ni stun [enemyStunPenaltyCountdown] retourne true si non false", category: "Conditions", id: "a8d111a77360a8a95cdf0627cef62cff")]
public partial class CheckEnemyIsHitCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> enemyHitPenaltyCountdown;
    [SerializeReference] public BlackboardVariable<float> enemyStunPenaltyCountdown;

    public override bool IsTrue()
    {
        if(enemyHitPenaltyCountdown == null || enemyStunPenaltyCountdown == null)
        {
            return false;
        }

        return enemyHitPenaltyCountdown.Value <= 0f && enemyStunPenaltyCountdown.Value <= 0f;
    }

}
