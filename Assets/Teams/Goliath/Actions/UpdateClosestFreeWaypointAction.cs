using System;
using System.Collections.Generic;
using DoNotModify;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateClosestFreeWaypoint", story: "Update the Closest Free Waypoint : resultat [Position], ship [Ship]", category: "Action", id: "d58d8408c5abd408fdb61930393e1851")]
public partial class UpdateClosestFreeWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector2> Position;
    [SerializeReference] public BlackboardVariable<SpaceShip> Ship;
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
        float minDist = float.MaxValue;
        WayPointView closest = waypoints[0];
        foreach (WayPointView waypoint in waypoints)
        {
            if (waypoint.Owner == Ship.Value.Owner) continue;
            float dist = Vector2.Distance(Ship.Value.Position, waypoint.Position);
            if (minDist > dist)
            {
                minDist = dist;
                closest = waypoint;
            }
        }
        Position.Value = closest.Position;
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

