using Common;
using Common.Pool;
using UnityEngine;

namespace ZombieShooter.Enemies
{
    public class EnemyFabric
    {
        private Pool<EnemyContainer> pool;

        private Quaternion leftRotation;
        private Quaternion rightRotation;
        
        public EnemyFabric(EnemiesConfig config, Transform enemiesHolder, int order)
        {
            var spawnedEnemies = new EnemyContainer[config.EnemyOriginals.Length];

            for (var i = 0; i < spawnedEnemies.Length; i++)
            {
                spawnedEnemies[i] = Object.Instantiate(config.EnemyOriginals[i], enemiesHolder);
                spawnedEnemies[i].transform.ResetLocal();
                spawnedEnemies[i].Order = order;
                
                spawnedEnemies[i].Initialize();
            }

            pool = new Pool<EnemyContainer>(spawnedEnemies);
            
            leftRotation = Quaternion.Euler(0, 0, 0);
            rightRotation = Quaternion.Euler(0, 180, 0);
        }

        public EnemyContainer Get(float position, bool leftSide)
        {
            var enemy = pool.GetObject();
            
            enemy.Position = position;
            enemy.transform.rotation = leftSide ? leftRotation : rightRotation;
            enemy.ResetHealth();
            
            return enemy;
        }

        public void Free(EnemyContainer container)
        {
            pool.FreeObject(container);
        }
    }
}