using System;
using System.Collections.Generic;
using DoNotModify;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateClosestFreeWaypoint", story: "Update the Closest Free Waypoint [Value]", category: "Action", id: "d58d8408c5abd408fdb61930393e1851")]
public partial class UpdateClosestFreeWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> Value;
    //[SerializeReference] public BlackboardVariable<Vector2> ;
    private GameData data;
    private List<WayPointView> waypoints;
    
    protected override Status OnStart()
    {
        data = GameManager.Instance.GetGameData();
        waypoints = data.WayPoints;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        foreach (WayPointView waypoint in waypoints)
        {
            
        }
        
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

