using System;
using System.Collections.Generic;
using Common;
using Common.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZombieShooter.Guns
{
    public class BulletController
    {
        public event Action<int> BulletCountChanged; 
        
        private BulletConfig config;
        
        private Transform holder;
        private IShootPoint shootPoint;
        
        private Pool<BaseBullet> pool;
        private LinkedList<BaseBullet> shootedBullets;
        private HashSet<BaseBullet> cachedBullets;
        
        private DateTime lastShotTime;
        private float bulletsCount;
        
        public BulletController(BulletConfig config, Transform holder, IShootPoint shootPoint)
        {
            this.config = config;
            
            this.holder = holder;
            this.shootPoint = shootPoint;
            
            var bulletObjects = new BaseBullet[config.PoolSize];
            
            for (var i = 0; i < config.PoolSize; i++)
            {
                bulletObjects[i] = Object.Instantiate(config.BulletOriginal, this.holder);
                bulletObjects[i].transform.position = holder.position;
                bulletObjects[i].SetOrder(config.BulletDrawOrder);
            }
            
            pool = new Pool<BaseBullet>(bulletObjects);
            shootedBullets = new LinkedList<BaseBullet>();
            cachedBullets = new HashSet<BaseBullet>(config.PoolSize);
            
            lastShotTime = DateTime.Now;
            bulletsCount = this.config.StartBulletCount;
        }
        
        public void Shoot()
        {
            if (bulletsCount <= 0 || DateTime.Now - lastShotTime < TimeSpan.FromSeconds(config.ShootInterval))
            {
                return;
            }
            
            var bullet = pool.GetObject();
            bullet.Shoot(shootPoint);
            bullet.Hitted += OnBulletHitted;
            
            shootedBullets.AddLast(bullet);
            lastShotTime = DateTime.Now;
            bulletsCount--;
        }

        private void OnBulletHitted(BaseBullet bullet)
        {
            bullet.Hitted -= OnBulletHitted;
            pool.FreeObject(bullet);
            
            shootedBullets.Remove(bullet);
        }

        public void UpdateSelf(float deltaTime)
        {
            foreach (var bullet in shootedBullets)
            {
                bullet.Move(deltaTime);

                if (bullet.CheckOutOfBorder(config.BulletBorder))
                {
                    bullet.Hitted -= OnBulletHitted;
                    
                    pool.FreeObject(bullet);
                    cachedBullets.Add(bullet);
                }
            }

            foreach (var cached in cachedBullets)
            {
                shootedBullets.Remove(cached);
            }
            
            cachedBullets.Clear();
        }
    }
}