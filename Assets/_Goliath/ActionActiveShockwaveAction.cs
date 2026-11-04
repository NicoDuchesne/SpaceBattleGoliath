using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Action Active Shockwave", story: "Action activer qui passe ShockWave en true [triggerShockwave]", category: "Action", id: "2a02bee7379add435b76cc579b43ea2c")]
public partial class ActionActiveShockwaveAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> triggerShockwave;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (triggerShockwave == null)
        {
            return Status.Failure;
        }

        triggerShockwave.Value = true;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

