using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Energy Mine", story: "Si [playerEnergy] > [checkMineEnergy]", category: "Conditions", id: "65eb3d5957906829069b9a91775243fb")]
public partial class CheckEnergyMineCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> playerEnergy;
    [SerializeReference] public BlackboardVariable<float> checkMineEnergy;

    public override bool IsTrue()
    {
        if (checkMineEnergy == null && playerEnergy == null)
        {
            return false;
        }

        return playerEnergy.Value >= checkMineEnergy.Value;
    }
}
