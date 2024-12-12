using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;

namespace ZombieShooter.Game.GameStates
{
    public class ShootState : GameState
    {
        public override GameStateType Type => GameStateType.Shoot;
        
        public ShootState(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController) : base(locationController, cameraController,
            gunnerController, bulletController) { }


        public override void OnEnter()
        {
            gunnerController.SetFire(true);
        }

        public override void UpdateInput(InputContainer input)
        {
            if (input.FirePressed)
            {
                bulletController.Shoot();

                if (input.LeftPressed)
                {
                    gunnerController.SetLeft();
                }

                if (input.RightPressed)
                {
                    gunnerController.SetRight();
                }
                
                return;
            }

            if (input.LeftPressed || input.RightPressed)
            {
                RaiseChangeState(GameStateType.Run);
                return;
            }
            
            RaiseChangeState(GameStateType.Idle);
        }

        public override void OnExit()
        {
            gunnerController.SetFire(false);
        }
    }
}