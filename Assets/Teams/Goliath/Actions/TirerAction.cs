using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Tirer", story: "Action de Tirer [needShoot] passe en true [timeSinceLastShot]", category: "Action", id: "f56c3af28aa685d7fe030336b9c1f010")]
    public partial class TirerAction : Action
    {
        [SerializeReference] public BlackboardVariable<bool> needShoot;
        [SerializeReference] public BlackboardVariable<float> timeSinceLastShot;

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

            if (timeSinceLastShot != null)
            {
                timeSinceLastShot.Value = 0f;
            }

            needShoot.Value = true;

            return Status.Success;
        }
    }
}