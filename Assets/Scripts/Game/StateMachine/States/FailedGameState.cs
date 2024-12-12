using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Location;

namespace ZombieShooter.Game.GameStates
{
    public class FailedGameState : GameState
    {
        public override GameStateType Type  => GameStateType.Failed;
        
        public FailedGameState(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController) : base(locationController,
            cameraController, gunnerController, bulletController)
        { }
    }
}