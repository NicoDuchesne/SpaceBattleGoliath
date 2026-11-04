using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Time Left Inferieur", story: "Check si [timeLeftGame] est inferieur à [timeCheckGame]", category: "Conditions", id: "5d62745bda70acd4c73bcfb697088dac")]
public partial class CheckTimeLeftSuperieurCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> timeLeftGame;
    [SerializeReference] public BlackboardVariable<float> timeCheckGame;

    public override bool IsTrue()
    {
        if (timeLeftGame == null || timeCheckGame == null)
        {
            return false;
        }

        return timeLeftGame.Value < timeCheckGame.Value;
    }
}
