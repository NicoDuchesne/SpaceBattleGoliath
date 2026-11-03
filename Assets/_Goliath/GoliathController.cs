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
        BlackboardVariable<bool> needShoot;
        BlackboardVariable<bool> triggerShockwave;
        BlackboardVariable<Vector2> posPlayer;
        BlackboardVariable<Vector2> posEnemy;


        public override void Initialize(SpaceShipView spaceship, GameData data) {
            behavior = GetComponent<BehaviorGraphAgent>();
            behavior.BlackboardReference.GetVariable("outRotateToward", out outRotateToward);
            behavior.BlackboardReference.GetVariable("thrust", out thrust);
            behavior.BlackboardReference.GetVariable("targetOrient", out targetOrient);
            behavior.BlackboardReference.GetVariable("playerEnergy", out playerEnergy);
            behavior.BlackboardReference.GetVariable("enemyHitPenaltyCountdown", out enemyHitPenaltyCountdown);
            behavior.BlackboardReference.GetVariable("needShoot", out needShoot);
            behavior.BlackboardReference.GetVariable("triggerShockwave", out triggerShockwave);
            behavior.BlackboardReference.GetVariable("posPlayer", out posPlayer);
            behavior.BlackboardReference.GetVariable("posEnemy", out posEnemy);
        }

        public override InputData UpdateInput(SpaceShipView spaceship, GameData data) {
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
            playerEnergy.Value = spaceship.Energy;
            enemyHitPenaltyCountdown.Value = otherSpaceship.HitPenaltyCountdown;
            posPlayer.Value = spaceship.Position;
            posEnemy.Value = otherSpaceship.Position;
            //thrust = 1.0f;
            //float targetOrient = 1f;

            // CanHit usage example, to estimate if a shot will hit enemy spaceship
            //if (Vector2.Distance(spaceship.Position, otherSpaceship.Position) < 1f)
            //{
            //    targetOrient = spaceship.Orientation + outRotateToward;
            //}
            //else
            //{
            //    float valueOrientation = AimingHelpers.ComputeSteeringOrient(spaceship, otherSpaceship.Position, 1f);
            //    targetOrient = valueOrientation;
            //}
            //bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
            //if(spaceship.Energy < 0.5f || otherSpaceship.HitPenaltyCountdown > 0f)
            //{
            //    needShoot = false;
            //}

            //bool triggerShockwave = false;

            //if (Vector2.Distance(spaceship.Position,otherSpaceship.Position) < 1f)
            //{
            //    triggerShockwave = true;
            //    needShoot = false;
            //}
            //bool needShoot = false;
            // Example of how to read and write a variable from the behavior graph's blackboard for trigger-like behaviors without using reference
            //behavior.BlackboardReference.GetVariableValue("triggerShockwave", out triggerShockwave);
            //behavior.BlackboardReference.SetVariableValue("triggerShockwave", false);

            // InputData is the only way to send input to the spaceship : thrust [0, 1], target orientation [0, 360], shoot, drop mine, fire shockwave
            // It will be send every frame.
            return new InputData(thrust, targetOrient, needShoot, false, triggerShockwave);
        }
    }

}
