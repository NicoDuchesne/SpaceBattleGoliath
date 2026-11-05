using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "EnergySuperiorTo", story: "[Energy] Superior To [Value]", category: "Conditions", id: "9d19c116f00c6677dc26efb5721b931d")]
public partial class EnergySuperiorToCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> Energy;
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        return Energy.Value > Value.Value;
    }
}
