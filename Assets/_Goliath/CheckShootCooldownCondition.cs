using System;
using Unity.Behavior;
using UnityEngine;

namespace GoliathTeam
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Check Shoot Cooldown", story: "Vérifier si le délai [cooldown] est passé depuis [timeSinceLastShot]", category: "Conditions", id: "ceb6c87c338c73897d0ffefc8074820e")]
    public partial class CheckShootCooldownCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<float> timeSinceLastShot;
        [SerializeReference] public BlackboardVariable<float> cooldown;

        public override bool IsTrue()
        {
            if (timeSinceLastShot == null || cooldown == null)
            {
                return false;
            }

            return timeSinceLastShot.Value >= cooldown.Value;
        }
    }
}