using System;
using UnityEngine;
using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Enemies;
using ZombieShooter.Game.StateMachine;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;


namespace ZombieShooter
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyboardInputSettings settings;
        [SerializeField] private LocationConfig locationConfig;
        [SerializeField] private Camera camera;
        [SerializeField] private GunnerContainer gunnerContainer;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        
        private IInput input;
        
        private LocationController locationController;
        private CameraController cameraController;
        private GunnerController characterController;
        private BulletController bulletController;
        private EnemiesController enemiesController;
        
        private GameStateMachine stateMachine;
        
        private void Start()
        {
            input = new KeyboardInput(settings);
            
            var locationFabric = new LocationFabric();
            var locationData = locationFabric.CreateLocation(locationConfig);
            
            locationController = new LocationController(locationData);
            cameraController = new CameraController(camera, locationConfig.CameraConfig);
            
            var character = locationController.SpawnCharacter(gunnerContainer);
            characterController = new GunnerController(character);
            
            bulletController = new BulletController(bulletConfig, locationData.EnemiesHolder, characterController);
            
            enemiesController = new EnemiesController(enemiesConfig, locationData.EnemiesHolder, locationData.EnemiesOrder);
            enemiesController.Start();
            enemiesController.EnemyReachedPlayer += OnEnemyReachedPlayer;
            
            stateMachine = new GameStateMachine(locationController, cameraController, characterController, bulletController);
        }

        private void OnEnemyReachedPlayer()
        {
            enemiesController.StopSpawn();
        }

        private void Update()
        {
            var inputContainer = input.GetInput();
            stateMachine.Update();
            stateMachine.UpdateInput(inputContainer);
            
            enemiesController.Update();
        }

        private void OnDestroy()
        {
            enemiesController.EnemyReachedPlayer -= OnEnemyReachedPlayer;
        }
    }
}