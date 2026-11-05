using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "DetectCloseEnemy", story: "Si [PosEnemy] est inférieur à [CloseDistanceValue]", category: "Action", id: "ecef6143e22761b4c2a2dd62be629f10")]
    public partial class DetectCloseEnemyAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> PosEnemy;
        [SerializeReference] public BlackboardVariable<float> CloseDistanceValue;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}
