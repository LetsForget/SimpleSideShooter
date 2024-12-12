using UnityEngine;

namespace ZombieShooter.Guns
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig", order = 2)]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public float ShootInterval { get; private set; }
        [field: SerializeField] public int PoolSize { get; private set; } 
        [field: SerializeField] public int BulletDrawOrder { get; private set; }
        [field: SerializeField] public float BulletBorder { get; private set; }
        [field: SerializeField] public BaseBullet BulletOriginal { get; private set; }
        [field: SerializeField] public float StartBulletCount { get; private set; }
    }
}