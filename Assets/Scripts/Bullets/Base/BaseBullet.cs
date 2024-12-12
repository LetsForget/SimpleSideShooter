using System;
using Common.Pool;
using UnityEngine;

namespace ZombieShooter.Guns
{
    public abstract class BaseBullet : MonoBehaviour, IPoolable
    {
        public event Action<BaseBullet> Hitted; 
        
        [SerializeField] private float damage;
        
        public abstract void Shoot(IShootPoint shootPoint);
        public abstract void Move(float deltaTime);
        public abstract bool CheckOutOfBorder(float border);
        public abstract void SetOrder(int order);
        
        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IBulletReceiver>(out var receiver))
            {
                receiver.ReceiveBullet(damage);
                Hitted?.Invoke(this);
            }
        }

        public abstract void OnTakenFromPool();

        public abstract void OnTakenBackToPool();
    }
}