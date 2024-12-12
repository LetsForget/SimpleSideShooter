using System;
using System.Collections.Generic;
using Common.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZombieShooter.Loot
{
    public class LootController
    {
        public event Action<int> Looted;
        
        private LootConfig config;
        
        private Pool<LootContainer> pool;
        private LinkedList<LootContainer> loots;
        private HashSet<LootContainer> cachedLoots;
        
        public LootController(LootConfig config, Transform lootHolder, int lootOrder)
        {
            this.config = config;
            
            var lootsContainers = new LootContainer[config.PoolSize];

            for (var i = 0; i < config.PoolSize; i++)
            {
                lootsContainers[i] = GameObject.Instantiate(config.LootOriginal, lootHolder);
                lootsContainers[i].transform.position = lootHolder.position;
                lootsContainers[i].Order = lootOrder;
            }
            
            pool = new Pool<LootContainer>(lootsContainers);
            
            loots = new LinkedList<LootContainer>();
            cachedLoots = new HashSet<LootContainer>();
        }

        public void OnEnemyDied(float position)
        {
            if (Random.value > config.LootChance)
            {
                return;
            }

            var loot = pool.GetObject();
            loot.Position = position;
            loot.BulletCount = Random.Range(config.MinBulletCount, config.MaxBulletCount);

            loots.AddLast(loot);
        }

        public void Update()
        {
            foreach (var loot in loots)
            {
                if (Mathf.Abs(loot.Position) < 0.2f)
                {
                    pool.FreeObject(loot);
                    cachedLoots.Add(loot);
                    
                    Looted?.Invoke(loot.BulletCount);
                }
            }

            foreach (var loot in cachedLoots)
            {
                loots.Remove(loot);
            }
            
            cachedLoots.Clear();
        }

        public void Reset()
        {
            foreach (var loot in loots)
            {
                pool.FreeObject(loot);
            }
            
            loots.Clear();
        }
    }
}