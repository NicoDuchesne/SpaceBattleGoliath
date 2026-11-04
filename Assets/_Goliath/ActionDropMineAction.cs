using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Action Drop Mine", story: "Pose la mine en passant en true [canDropMine]", category: "Action", id: "12a36b3968494b8bfc21831d8a39df4a")]
public partial class ActionDropMineAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> canDropMine;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if(canDropMine == null)
        {
            return Status.Failure;
        }

        canDropMine.Value = true;
        return Status.Success;
    }
}

