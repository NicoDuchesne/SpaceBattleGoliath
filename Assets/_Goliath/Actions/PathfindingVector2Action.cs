using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace GoliathTeam
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PathfindingVector2", story: "Pathfinding to [position]", category: "Action", id: "49dc10e18d87551ce80c69d54726cf51")]
    public partial class PathfindingVector2Action : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Position;
        private NavGrid nav;
    
        protected override Status OnStart()
        {
            nav = GameObject.GetComponent<NavGrid>();
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            nav.SetTarget(Position.Value);
            nav.Play();
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}



