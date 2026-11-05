using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Action Stop Poser Mine", story: "Stop Poser la mine en passant en false [canDropMine]", category: "Action", id: "3a127f6e20bc247737fb260218f99383")]
    public partial class ActionStopPoserMineAction : Action
    {
        [SerializeReference] public BlackboardVariable<bool> canDropMine;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (canDropMine == null)
            {
                return Status.Failure;
            }

            canDropMine.Value = false;
            return Status.Success;
        }
    }
}
