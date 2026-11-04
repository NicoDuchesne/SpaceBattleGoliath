using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetBoolOneFrame", story: "Set [Bool] true during one frame", category: "Action", id: "be5ab09c572ca111f25da8a968775e14")]
public partial class SetBoolOneFrame : Action
{
    [SerializeReference] public BlackboardVariable<bool> Bool;
    private int _frames;
    
    protected override Status OnStart()
    {
        Bool.Value = true;
        _frames = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (++_frames < 2) return Status.Running;
        Bool.Value = false;
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Bool.Value = false; // sécurité si le nœud est interrompu
    }
}

