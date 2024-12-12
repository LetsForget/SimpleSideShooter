using UnityEngine;

namespace ZombieShooter.Inputs
{
    [CreateAssetMenu(fileName = "KeyboardInputSettings", menuName = "Configs/Keyboard Input Settings")]
    public class KeyboardInputSettings : ScriptableObject
    {
        [field: SerializeField] public KeyCode FireButton { get; private set; }
        [field: SerializeField] public KeyCode LeftButton { get; private set; }
        [field: SerializeField] public KeyCode RightButton { get; private set; }
    }
}