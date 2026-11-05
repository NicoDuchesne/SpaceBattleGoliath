using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckMineCooldown", story: "Vérifier si le délai [cooldownMine] est passé depuis [timeSinceLastMine]", category: "Conditions", id: "c47eb5a49d963f1db07355abc7f6586c")]
public partial class CheckMineCooldownCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> timeSinceLastMine;
    [SerializeReference] public BlackboardVariable<float> cooldownMine;

    public override bool IsTrue()
    {
        if (timeSinceLastMine == null || cooldownMine == null)
        {
            return false;
        }

        return timeSinceLastMine.Value >= cooldownMine.Value;
    }
}
