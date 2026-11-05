using System;
using Unity.Behavior;
using UnityEngine;

namespace GoliathTeam
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckEnergyShockwave", story: "Si [playerEnergy] > [checkEnergyShockwave] ", category: "Conditions", id: "1ae9d66c212d1f2dc4599143c24ef6ed")]
    public partial class CheckEnergyShockwaveCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<float> checkEnergyShockwave;
        [SerializeReference] public BlackboardVariable<float> playerEnergy;

        public override bool IsTrue()
        {
            if (checkEnergyShockwave == null && playerEnergy == null)
            {
                return false;
            }

            return playerEnergy.Value >= checkEnergyShockwave.Value;
        }
    }
}