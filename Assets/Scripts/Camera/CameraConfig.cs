using UnityEngine;

namespace ZombieShooter.Cameras
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
        [field: SerializeField] public float Size { get; private set; }
    }
}