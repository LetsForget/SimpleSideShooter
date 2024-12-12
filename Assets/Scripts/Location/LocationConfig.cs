using UnityEngine;
using ZombieShooter.Cameras;

namespace ZombieShooter.Location
{
    [CreateAssetMenu(fileName = "LocationConfig", menuName = "Configs/LocationConfig", order = 1)]
    public class LocationConfig : ScriptableObject
    {
        [field: SerializeField] public Block MainBackground { get; private set; }
        [field: SerializeField] public Block Floor { get; private set; }
        
        [field: SerializeField] public Block[] BackgroundBlocks { get; private set; }
        [field: SerializeField] public Block[] MidgroundBlocks { get; private set; }
        [field: SerializeField] public Block[] ForegroundBlocks { get; private set; }
        [field: SerializeField] public float BlocksBorder { get; private set; }
        
        [field: SerializeField] public float CharactersGroundLevel { get; private set; }
        
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
    }
}