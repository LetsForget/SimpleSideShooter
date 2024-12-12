using Common.Pool;
using UnityEngine;
using ZombieShooter.Guns;

namespace ZombieShooter.Enemies
{
    public class EnemyContainer : MonoBehaviour, IBulletReceiver, IPoolable
    {
        private static readonly int Health = Shader.PropertyToID("_Health");

        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private SpriteRenderer healthBar;
        [SerializeField] private Collider collision;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject holder;
        [SerializeField] private float startHealth;
        
        [field: SerializeField] public float Speed { get; private set; }
        
        
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value; 
                healthBar.material.SetFloat(Health, _currentHealth / startHealth);
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
                sprite.sortingOrder = value;
                healthBar.sortingOrder = value;
            } 
        }

        public void Initialize()
        {
            healthBar.material = Instantiate(healthBar.material);
        }
        
        public void ReceiveBullet(float damage) => CurrentHealth -= damage;
        
        public void ResetHealth() => CurrentHealth = startHealth;
        
        public void SetAnimationSpeed(float value) => animator.speed = value;

        public void OnTakenFromPool()
        {
            holder.gameObject.SetActive(true);
            collision.enabled = true;
        }

        public void OnTakenBackToPool()
        {
            holder.gameObject.SetActive(false);
            collision.enabled = false;
        }
    }
}