using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Action Stop ShockWave", story: "Action de Stop de ShockWave passe [triggerShockwave] passe en false", category: "Action", id: "820dd310d208edbf8866613c821db302")]
    public partial class ActionStopShockWaveAction : Action
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

            triggerShockwave.Value = false;

            return Status.Success;
        }
    }
}

