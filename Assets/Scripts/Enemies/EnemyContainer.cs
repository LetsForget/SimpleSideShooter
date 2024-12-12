using Common.Pool;
using UnityEngine;
using ZombieShooter.Guns;

namespace ZombieShooter.Enemies
{
    public class EnemyContainer : MonoBehaviour, IBulletReceiver, IPoolable
    {
        private static readonly int Health = Shader.PropertyToID("_Health");
        
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public SpriteRenderer HealthBar { get; private set; }
        [field: SerializeField] public Collider Collision { get; private set; }
        
        [field: SerializeField] public GameObject Holder { get; private set; }
        [field: SerializeField] public float StartHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        
        
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value; 
                HealthBar.material.SetFloat(Health, _currentHealth / StartHealth);
            }
        }
        
        public float Position
        {
            get => transform.position.x;
            set => transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        
        public int Order
        {
            set
            {
                SpriteRenderer.sortingOrder = value;
                HealthBar.sortingOrder = value;
            } 
        }

        public void Initialize()
        {
            HealthBar.material = Instantiate(HealthBar.material);
        }
        
        public void ReceiveBullet(float damage) => CurrentHealth -= damage;
        
        public void ResetHealth() => CurrentHealth = StartHealth;
        
        public void OnTakenFromPool()
        {
            Holder.gameObject.SetActive(true);
            Collision.enabled = true;
        }

        public void OnTakenBackToPool()
        {
            Holder.gameObject.SetActive(false);
            Collision.enabled = false;
        }
    }
}