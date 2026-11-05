using System;
using Unity.Behavior;
using UnityEngine;


namespace GoliathTeam
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Check Take Waypoint", story: "Check si [waypointScore] est different de [oldWaypointScore] si different return true", category: "Conditions", id: "0be30b0e8758e78b2ef86c86d776c767")]
    public partial class CheckTakeWaypointCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<int> WaypointScore;
        [SerializeReference] public BlackboardVariable<int> OldWaypointScore;

        public override bool IsTrue()
        {
            if (WaypointScore == null || OldWaypointScore == null)
            {
                return false;
            }

            int currentScore = WaypointScore.Value;
            int previousScore = OldWaypointScore.Value;

            if (currentScore != previousScore)
            {
                OldWaypointScore.Value = currentScore;

                return currentScore > previousScore;
            }

            return false;
        }
    }
}

