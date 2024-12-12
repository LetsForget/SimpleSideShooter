using UnityEngine;
using ZombieShooter.Cameras;
using ZombieShooter.Gunner;
using ZombieShooter.Enemies;
using ZombieShooter.Game;
using ZombieShooter.Game.StateMachine;
using ZombieShooter.Guns;
using ZombieShooter.Inputs;
using ZombieShooter.Location;
using ZombieShooter.Loot;
using ZombieShooter.UI;


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
        [SerializeField] private LootConfig lootConfig;
        [SerializeField] private UIPanel uiPanel;
        
        private IInput input;
        
        private LocationController locationController;
        private CameraController cameraController;
        private GunnerController characterController;
        private BulletController bulletController;
        private EnemiesController enemiesController;
        private LootController lootController;
        
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
            bulletController.BulletsCountChanged += OnBulletsCountChanged;
            OnBulletsCountChanged(bulletConfig.StartBulletCount);
            
            enemiesController = new EnemiesController(enemiesConfig, locationData.EnemiesHolder, locationData.EnemiesOrder);
            enemiesController.StartSpawn();
            enemiesController.EnemyReachedPlayer += OnEnemyReachedPlayer;
            enemiesController.EnemyDied += OnEnemyDied;
            
            lootController = new LootController(lootConfig, locationData.EnemiesHolder, locationData.EnemiesOrder);
            lootController.Looted += OnLooted;
            
            stateMachine = new GameStateMachine(locationController, cameraController, characterController, bulletController);
        }
        
        private void Update()
        {
            var inputContainer = input.GetInput();
            stateMachine.Update();
            stateMachine.UpdateInput(inputContainer);
            
            enemiesController.Update();
            lootController.Update();
        }

        private void OnDestroy()
        {
            bulletController.BulletsCountChanged -= OnBulletsCountChanged;
            
            enemiesController.EnemyReachedPlayer -= OnEnemyReachedPlayer;
            enemiesController.EnemyDied -= OnEnemyDied;
            
            lootController.Looted -= OnLooted;
        }
        
        private void OnLooted(int lootedBullets)
        {
            bulletController.OnLooted(lootedBullets);
        }

        private void OnEnemyDied(float position)
        {
            lootController.OnEnemyDied(position);
        }
        
        private void OnBulletsCountChanged(int count)
        {
            uiPanel.SetBulletsCount(count);
            
            if (count == 0)
            {
                GameOver();
            }
        }

        private void OnEnemyReachedPlayer()
        {
            GameOver();
        }

        private void GameOver()
        {
            stateMachine.ChangeState(GameStateType.Failed);
            
            enemiesController.Stop();
            uiPanel.OpenLosePanel(RestartGame, CloseGame);
        }

        private void RestartGame()
        {
            uiPanel.ClosePanel();
            
            locationController.Reset();
            bulletController.Reset();
            enemiesController.Reset();
            
            stateMachine.ChangeState(GameStateType.Idle);
        }

        private void CloseGame()
        {
            uiPanel.ClosePanel();
            
            Application.Quit();
        }
    }
}