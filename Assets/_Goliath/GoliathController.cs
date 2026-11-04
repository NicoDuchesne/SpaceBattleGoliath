using DoNotModify;
using Unity.Behavior;
using UnityEngine;

namespace GoliathTeam {

    public class GoliathController : BaseSpaceShipController {
        BehaviorGraphAgent behavior;
        BlackboardVariable<float> outRotateToward;
        BlackboardVariable<float> thrust;
        BlackboardVariable<float> targetOrient;
        BlackboardVariable<float> playerEnergy;
        BlackboardVariable<float> enemyHitPenaltyCountdown;
        BlackboardVariable<float> enemyStunPenaltyCountdown;
        BlackboardVariable<bool> needShoot;
        BlackboardVariable<bool> triggerShockwave;
        BlackboardVariable<Vector2> posPlayer;
        BlackboardVariable<Vector2> posEnemy;
        BlackboardVariable<float> playerOrientation;
        BlackboardVariable<bool> canHit;
        BlackboardVariable<int> waypointScore;
        BlackboardVariable<bool> canDropMine;

        public override void Initialize(SpaceShipView spaceship, GameData data) {
            behavior = GetComponent<BehaviorGraphAgent>();
            behavior.BlackboardReference.GetVariable("outRotateToward", out outRotateToward);
            behavior.BlackboardReference.GetVariable("thrust", out thrust);
            behavior.BlackboardReference.GetVariable("targetOrient", out targetOrient);
            behavior.BlackboardReference.GetVariable("playerEnergy", out playerEnergy);
            behavior.BlackboardReference.GetVariable("enemyHitPenaltyCountdown", out enemyHitPenaltyCountdown);
            behavior.BlackboardReference.GetVariable("enemyStunPenaltyCountdown", out enemyStunPenaltyCountdown);
            behavior.BlackboardReference.GetVariable("needShoot", out needShoot);
            behavior.BlackboardReference.GetVariable("triggerShockwave", out triggerShockwave);
            behavior.BlackboardReference.GetVariable("posPlayer", out posPlayer);
            behavior.BlackboardReference.GetVariable("posEnemy", out posEnemy);
            behavior.BlackboardReference.GetVariable("playerOrientation", out playerOrientation);
            behavior.BlackboardReference.GetVariable("canHit", out canHit);
            behavior.BlackboardReference.GetVariable("waypointScore", out waypointScore);
            behavior.BlackboardReference.GetVariable("canDropMine", out canDropMine);

        }

        public override InputData UpdateInput(SpaceShipView spaceship, GameData data) {
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);

            playerEnergy.Value = spaceship.Energy;
            enemyHitPenaltyCountdown.Value = otherSpaceship.HitPenaltyCountdown;
            enemyStunPenaltyCountdown.Value = otherSpaceship.StunPenaltyCountdown;
            posPlayer.Value = spaceship.Position;
            posEnemy.Value = otherSpaceship.Position;
            playerOrientation.Value = spaceship.Orientation;
            waypointScore.Value = spaceship.WaypointScore;

            return new InputData(thrust.Value, targetOrient.Value, needShoot.Value, canDropMine, triggerShockwave.Value);
        }
    }

}
