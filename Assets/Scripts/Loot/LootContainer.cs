using Common.Pool;
using UnityEngine;

namespace ZombieShooter.Loot
{
    public class LootContainer : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer sprite;

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
            } 
        }
        
        public int BulletCount { get; set; }
        
        public void OnTakenFromPool()
        {
            sprite.enabled = true;
        }

        public void OnTakenBackToPool()
        {
            sprite.enabled = false;
        }
    }
}