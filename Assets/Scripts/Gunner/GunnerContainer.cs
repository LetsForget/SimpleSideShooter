using UnityEngine;

namespace ZombieShooter.Gunner
{
    public class GunnerContainer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        
        public int Order
        {
            set => sprite.sortingOrder = value;
        }

    }
}