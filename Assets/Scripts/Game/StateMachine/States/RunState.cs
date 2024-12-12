using UnityEngine;
using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;


namespace ZombieShooter.Game.GameStates
{
    public class RunState : GameState
    {
        public override GameStateType Type => GameStateType.Run;
        
        private float MoveDelta => gunnerController.Speed * Time.deltaTime;
        
        public RunState(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController) : base(locationController, cameraController,
            gunnerController, bulletController) { }

        public override void OnEnter()
        {
            gunnerController.SetRun(true);
        }

        public override void UpdateInput(InputContainer input)
        {
            if (input.FirePressed)
            {
                RaiseChangeState(GameStateType.Shoot);
                return;
            }
            
            if (input.LeftPressed)
            {
                locationController.Move(-MoveDelta);
                gunnerController.SetLeft();
                return;
            }
            
            if (input.RightPressed)
            {
                locationController.Move(MoveDelta);
                gunnerController.SetRight();
                return;
            }
            
            RaiseChangeState(GameStateType.Idle);
        }
        
        public override void OnExit()
        {
            gunnerController.SetRun(false);
        }
    }
}