using System;
using Unity.Behavior;
using UnityEngine;

namespace GoliathTeam
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Check PlayerEnergy Inferieur", story: "Check [playerEnergy] < [compareEnergyPlayer]", category: "Conditions", id: "212e3874300bb69823de1d2b3752a2ad")]
    public partial class CheckPlayerEnergyInferieurCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<float> playerEnergy;
        [SerializeReference] public BlackboardVariable<float> compareEnergyPlayer;


        public override bool IsTrue()
        {
            if (playerEnergy == null || compareEnergyPlayer == null)
            {
                return false;
            }

            return playerEnergy.Value < compareEnergyPlayer;
        }
    }
}