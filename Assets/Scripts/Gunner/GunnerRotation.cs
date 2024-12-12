using UnityEngine;

namespace ZombieShooter.Gunner
{
    public class GunnerRotation
    {
        private Transform transform;
        
        private Quaternion leftRotation;
        private Quaternion rightRotation;
        
        public GunnerRotation(Transform transform)
        {
            this.transform = transform;

            leftRotation = Quaternion.Euler(0, 0, 0);
            rightRotation = Quaternion.Euler(0, 180, 0);
        }

        public void TurnLeft() => SetRotation(leftRotation);
        public void TurnRight() => SetRotation(rightRotation);

        
        private void SetRotation(Quaternion rotation) => transform.rotation = rotation;
    }
}