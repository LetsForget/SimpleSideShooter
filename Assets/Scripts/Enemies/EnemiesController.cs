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

        public void Start()
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
                    enemyFabric.Take(enemy);
                    cachedEnemies.Add(enemy);
                }
                else
                {
                    var moveDelta = enemy.Speed * deltaTime;

                    if (enemy.Position > 0)
                    {
                        moveDelta = -moveDelta;
                    }
                
                    if (Mathf.Abs(moveDelta) > Mathf.Abs(enemy.Position))
                    {
                        EnemyReachedPlayer?.Invoke();
                    
                        StopSpawn();
                        paused = true;
                    }
                    else
                    {
                        enemy.Position += moveDelta;
                    }
                }
            }

            foreach (var enemy in cachedEnemies)
            {
                enemies.Remove(enemy);
            }
            
            cachedEnemies.Clear();
        }
        
        public void StopSpawn()
        {
            cts.Cancel();
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
                
                enemies.AddLast(enemy);
            }
        }
    }
}