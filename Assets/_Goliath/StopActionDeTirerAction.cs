using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop Action de Tirer", story: "Action de Stop de tir passe en true[needShoot] passe en false", category: "Action", id: "058bf4998e8e25a500576d4c4992eb8f")]
public partial class StopActionDeTirerAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> needShoot;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (needShoot == null)
        {
            return Status.Failure;
        }

        needShoot.Value = false;

        return Status.Success;
    }
}

