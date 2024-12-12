using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZombieShooter.Enemies
{
    public class EnemiesController
    {
        public event Action EnemyReachedPlayer;
        public event Action<float> EnemyDied; 
        
        private EnemiesConfig config;
        private EnemyFabric enemyFabric;
        
        private LinkedList<EnemyContainer> enemies;
        private HashSet<EnemyContainer> cachedEnemies;
        
        private bool paused;
        
        private CancellationTokenSource cts;
        private Task spawnTask;
        
        public EnemiesController(EnemiesConfig config, Transform enemiesHolder, int order)
        {
            this.config = config;

            enemyFabric = new EnemyFabric(this.config, enemiesHolder, order);
            enemies = new LinkedList<EnemyContainer>();
            cachedEnemies = new HashSet<EnemyContainer>();
            
            cts = new CancellationTokenSource();
        }

        public void StartSpawn()
        {
            spawnTask = Spawn();
        }

        public void Update()
        {
            if (paused)
            {
                return;
            }
            
            var deltaTime = Time.deltaTime;
            
            foreach (var enemy in enemies)
            {
                if (enemy.CurrentHealth < 0)
                {
                    enemyFabric.Free(enemy);
                    cachedEnemies.Add(enemy);
                    
                    EnemyDied?.Invoke(enemy.Position);
                }
                else
                {
                    MoveEnemy(enemy, deltaTime);
                }
            }

            foreach (var enemy in cachedEnemies)
            {
                enemies.Remove(enemy);
            }
            
            cachedEnemies.Clear();
        }

        private void MoveEnemy(EnemyContainer enemy, float deltaTime)
        {
            var moveDelta = enemy.Speed * deltaTime;

            if (enemy.Position > 0)
            {
                moveDelta = -moveDelta;
            }
                    
            if (Mathf.Abs(enemy.Position) < 0.2f)
            {
                EnemyReachedPlayer?.Invoke();
            }
            else
            {
                enemy.Position += moveDelta;
            }
        }
        
        public void Stop()
        {
            cts.Cancel();
            paused = true;

            foreach (var enemy in enemies)
            {
                enemy.SetAnimationSpeed(0);
            }
        }
        
        private async Task Spawn()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                var delay = Random.Range(config.SpawnMinTime, config.SpawnMaxTime);
                var leftSide = Random.value < 0.5;
                await Task.Delay(TimeSpan.FromSeconds(delay), cts.Token);

                var spawnPosition = leftSide ? config.LeftSpawnPosition : config.RightSpawnPosition;
                var enemy = enemyFabric.Get(spawnPosition, leftSide);
                
                enemy.SetAnimationSpeed(1);
                
                enemies.AddLast(enemy);
            }
        }

        public void Reset()
        {
            foreach (var enemy in enemies)
            {
                enemyFabric.Free(enemy);
            }
            
            enemies.Clear();
            
            cts.Dispose();
            cts = new CancellationTokenSource();
            
            paused = false;
            
            StartSpawn();
        }
    }
}