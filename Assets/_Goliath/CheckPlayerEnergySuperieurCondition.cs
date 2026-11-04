using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckPlayerEnergy Superieur", story: "Check [playerEnergy] > [compareEnergyPlayer]", category: "Conditions", id: "37c5e80c5f95d2bcdf421788e77513fd")]
public partial class CheckPlayerEnergySuperieurCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> playerEnergy;
    [SerializeReference] public BlackboardVariable<float> compareEnergyPlayer;


    public override bool IsTrue()
    {
        if (playerEnergy == null || compareEnergyPlayer == null)
        {
            return false;
        }

        return playerEnergy.Value > compareEnergyPlayer;
    }

}
