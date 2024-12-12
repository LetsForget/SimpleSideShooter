using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;

namespace ZombieShooter.Game.GameStates
{
    public class IdleState : GameState
    {
        public override GameStateType Type => GameStateType.Idle;

        public IdleState(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController) : base(locationController, cameraController, gunnerController, bulletController) { }


        public override void UpdateInput(InputContainer input)
        {
            if (input.FirePressed)
            {
                RaiseChangeState(GameStateType.Shoot);
                return;
            }

            if (input.LeftPressed || input.RightPressed)
            {
                RaiseChangeState(GameStateType.Run);
                return;
            }
        }
    }
}