using System.Collections.Generic;
using Common.StateMachine;
using ZombieShooter.Cameras;
using ZombieShooter.Game.GameStates;
using ZombieShooter.Gunner;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;

namespace ZombieShooter.Game.StateMachine
{
    public class GameStateMachine : StateMachine<GameStateType, GameState>
    {
        public GameStateMachine(LocationController locationController, CameraController cameraController,
            GunnerController gunnerController, BulletController bulletController)
        {
            states = new Dictionary<GameStateType, GameState>
            {
                { GameStateType.Idle, new IdleState(locationController, cameraController, gunnerController, bulletController) },
                { GameStateType.Run, new RunState(locationController, cameraController, gunnerController, bulletController) },
                { GameStateType.Shoot, new ShootState(locationController, cameraController, gunnerController, bulletController) },
                { GameStateType.Failed, new FailedGameState(locationController, cameraController, gunnerController, bulletController)}
            };
            
            InitializeStates();
            
            ChangeState(GameStateType.Idle);
        }

        public void UpdateInput(InputContainer inputContainer)
        {
            currentState?.UpdateInput(inputContainer);
        }
    }
}