using UnityEngine;

namespace ZombieShooter.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyContainer[] EnemyOriginals { get; private set; }
        
        [field: SerializeField] public float LeftSpawnPosition { get; private set; }
        [field: SerializeField] public float RightSpawnPosition { get; private set; }

        [field: SerializeField] public float SpawnMinTime { get; private set; }
        [field: SerializeField] public float SpawnMaxTime { get; private set; }
    }
}