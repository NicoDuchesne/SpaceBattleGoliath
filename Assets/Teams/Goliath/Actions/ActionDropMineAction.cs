using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Action Drop Mine", story: "Pose la mine en passant en true [canDropMine] [timeSinceLastMine]", category: "Action", id: "12a36b3968494b8bfc21831d8a39df4a")]
    public partial class ActionDropMineAction : Action
    {
        [SerializeReference] public BlackboardVariable<bool> canDropMine;
        [SerializeReference] public BlackboardVariable<float> timeSinceLastMine;


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

            if (timeSinceLastMine != null)
            {
                timeSinceLastMine.Value = 0f;
            }

            canDropMine.Value = true;
            return Status.Success;
        }
    }
}
