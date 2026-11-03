using DoNotModify;
using Unity.Behavior;

namespace ExampleTeam {

    public class ExampleController : BaseSpaceShipController {
        BehaviorGraphAgent behavior;
        BlackboardVariable<float> outRotateToward;

        public override void Initialize(SpaceShipView spaceship, GameData data) {
            behavior = GetComponent<BehaviorGraphAgent>();
            behavior.BlackboardReference.GetVariable("outRotateToward", out outRotateToward);
        }

        public override InputData UpdateInput(SpaceShipView spaceship, GameData data) {
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
            float thrust = 1.0f;

            // Example of how to read a variable from the behavior graph's blackboard using a BlackboardVariable reference.
            float targetOrient = spaceship.Orientation + outRotateToward;

            // CanHit usage example, to estimate if a shot will hit enemy spaceship
            bool needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);

            // Example of how to read and write a variable from the behavior graph's blackboard for trigger-like behaviors without using reference
            behavior.BlackboardReference.GetVariableValue("triggerShockwave", out bool triggerShockwave);
            behavior.BlackboardReference.SetVariableValue("triggerShockwave", false);

            // InputData is the only way to send input to the spaceship : thrust [0, 1], target orientation [0, 360], shoot, drop mine, fire shockwave
            // It will be send every frame.
            return new InputData(thrust, targetOrient, needShoot, false, triggerShockwave);
        }
    }

}
