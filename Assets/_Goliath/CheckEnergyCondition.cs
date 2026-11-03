using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckEnergy", story: "Si [playerEnergy] est supérieur à [checkEnergy]", category: "Conditions", id: "e23db3bceefda8743717ee1ec8d103af")]
public partial class CheckEnergyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> playerEnergy;
    [SerializeReference] public BlackboardVariable<float> checkEnergy;

    public override bool IsTrue()
    {
        if (playerEnergy == null || checkEnergy == null)
        {
            return false;
        }

        return playerEnergy.Value >= checkEnergy.Value;
    }
}
