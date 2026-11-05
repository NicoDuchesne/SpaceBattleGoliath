using DoNotModify;
using System.Reflection;
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
        BlackboardVariable<float> hitTimeTolerance;
        BlackboardVariable<SpaceShip> playerVaisseau;
        BlackboardVariable<float> angleTolerance;
        BlackboardVariable<Vector2> enemyVelocity;
        BlackboardVariable<float> timeLeftGame;
        BlackboardVariable<SpaceShip> enemyVaisseau;
        BlackboardVariable<float> timeSinceLastShot;
        BlackboardVariable<float> timeSinceLastMine;
        BlackboardVariable<int> currentOwner;


        private static readonly FieldInfo SpaceShipField = typeof(SpaceShipView).GetField("_spaceship", BindingFlags.NonPublic | BindingFlags.Instance);//pour recup  le SpaceShip

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
            behavior.BlackboardReference.GetVariable("hitTimeTolerance", out hitTimeTolerance);
            behavior.BlackboardReference.GetVariable("playerVaisseau", out playerVaisseau);
            behavior.BlackboardReference.GetVariable("enemyVelocity", out enemyVelocity);
            behavior.BlackboardReference.GetVariable("timeLeftGame", out timeLeftGame);
            behavior.BlackboardReference.GetVariable("enemyVaisseau", out enemyVaisseau);
            behavior.BlackboardReference.GetVariable("angleTolerance", out angleTolerance);
            behavior.BlackboardReference.GetVariable("timeSinceLastShot", out timeSinceLastShot);
            behavior.BlackboardReference.GetVariable("timeSinceLastMine", out timeSinceLastMine);
            behavior.BlackboardReference.GetVariable("currentOwner", out currentOwner);
            

            if (playerVaisseau != null && SpaceShipField != null)
            {
                playerVaisseau.Value = SpaceShipField.GetValue(spaceship) as SpaceShip;
            }

            if (angleTolerance != null && angleTolerance.Value == 0)
            {
                angleTolerance.Value = 10.0f; 
            }

            if (timeLeftGame != null)
            {
                timeLeftGame.Value = data.timeLeft;
            }

            triggerShockwave.Value = false;
            canDropMine.Value = false;
            needShoot.Value = false;
            thrust.Value = 1f;

            if (timeSinceLastMine != null)
            {
                timeSinceLastMine.Value = data.timeLeft;
            }

            if (timeSinceLastShot != null)
            {
                timeSinceLastShot.Value = 0f;
            }
            
            if(currentOwner != null)
            {
                currentOwner.Value = spaceship.Owner;
            }
            
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);

            if (posPlayer != null) posPlayer.Value = spaceship.Position;
            if (posEnemy != null) posEnemy.Value = otherSpaceship.Position;
            if (enemyVelocity != null) enemyVelocity.Value = otherSpaceship.Velocity;
            if (timeLeftGame != null) timeLeftGame.Value = data.timeLeft;
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
            enemyVelocity.Value = otherSpaceship.Velocity;


            if (timeLeftGame != null)
            {
                timeLeftGame.Value = data.timeLeft;
            }

            if(timeSinceLastShot != null)
            {
                timeSinceLastShot.Value += Time.deltaTime;
                
            }

            if (timeSinceLastMine != null)
            {
                timeSinceLastMine.Value += Time.deltaTime;
            }
            
            return new InputData(thrust, targetOrient, needShoot, canDropMine, triggerShockwave);
        }
    }

}
