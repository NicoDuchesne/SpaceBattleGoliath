using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetFloat", story: "Set [Float] a new [Value]", category: "Action", id: "ec903626695436aa869d8f887f3b4136")]
public partial class SetFloat : Action
{
    [SerializeReference] public BlackboardVariable<float> Float;
    [SerializeReference] public BlackboardVariable<float> Value;

    protected override Status OnStart()
    {
        Float.Value = Value.Value;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

