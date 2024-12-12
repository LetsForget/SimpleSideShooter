using UnityEngine;

namespace ZombieShooter.Loot
{
    [CreateAssetMenu(fileName = "LootConfig", menuName = "Configs/LootConfig")]
    public class LootConfig :ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float lootChance;
        public float LootChance => lootChance;
        
        [field: SerializeField] public int PoolSize { get; private set; }
        [field: SerializeField] public LootContainer LootOriginal { get; private set; }
        [field: SerializeField] public int MinBulletCount { get; private set; }
        [field: SerializeField] public int MaxBulletCount { get; private set; }
    }
}