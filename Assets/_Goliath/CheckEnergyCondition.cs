using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckEnergy", story: "Si [playerEnergy] est supérieur à [checkEnergy] ou [checkMineEnergy]", category: "Conditions", id: "e23db3bceefda8743717ee1ec8d103af")]
public partial class CheckEnergyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> playerEnergy;
    [SerializeReference] public BlackboardVariable<float> checkEnergy;
    [SerializeReference] public BlackboardVariable<float> checkMineEnergy;


    public override bool IsTrue()
    {
        if (checkEnergy == null && checkMineEnergy == null)
        {
            return false;
        }

        bool isMineCheck = false;

        if (checkEnergy > 0f)
        {
            isMineCheck = false;
        }
        else
        {
            isMineCheck = true;
        }

        if (isMineCheck)
        {
            return playerEnergy.Value >= checkMineEnergy.Value;
        }
        else
        {
            return playerEnergy.Value >= checkEnergy.Value;
        }
    }
}
