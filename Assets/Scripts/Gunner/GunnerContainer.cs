using UnityEngine;

namespace ZombieShooter.Gunner
{
    public class GunnerContainer : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}