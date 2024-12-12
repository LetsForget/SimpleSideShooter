using UnityEngine;

namespace ZombieShooter.Guns
{
    public interface IShootPoint
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }
    }
}