using Common.StateMachine;
using UnityEngine;
using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;

namespace ZombieShooter.Game
{
    public abstract class GameState : State<GameStateType>
    {
        protected LocationController locationController;
        protected CameraController cameraController;
        protected GunnerController  gunnerController;
        protected BulletController bulletController;
        
        protected GameState(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController)
        {
            this.locationController = locationController;
            this.cameraController = cameraController;
            this.gunnerController = gunnerController;
            this.bulletController = bulletController;
        }

        public override void Update()
        {
            bulletController.UpdateSelf(Time.deltaTime);
        }
        
        public virtual void UpdateInput(InputContainer input) { }
    }
}