using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Take Waypoint", story: "Check si [waypointScore] et different de [oldWaypointScore] si different return true", category: "Conditions", id: "0be30b0e8758e78b2ef86c86d776c767")]
public partial class CheckTakeWaypointCondition : Condition
{
    [SerializeReference] public BlackboardVariable<int> WaypointScore;
    [SerializeReference] public BlackboardVariable<int> OldWaypointScore;

    public override bool IsTrue()
    {
        if(WaypointScore == null || OldWaypointScore == null)
        {
            return false;
        }

        if(WaypointScore.Value != OldWaypointScore.Value)
        {
            OldWaypointScore.Value = WaypointScore.Value;
            return true;
        }
        else
        {
            return false;
        }

    }
}
