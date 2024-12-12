using UnityEngine;
using ZombieShooter.Guns;

namespace ZombieShooter.Gunner
{
    public class GunnerController : IShootPoint
    {
        private GunnerContainer container;
        
        private GunnerAnimation animation;
        private GunnerRotation rotation;
        
        private Transform shootPoint;

        public Vector3 Position => shootPoint.position;
        public Vector3 Direction => shootPoint.right;
        public float Speed => container.Speed;

        public GunnerController(GunnerContainer container)
        {
            this.container = container;
            
            animation = new GunnerAnimation(container.Animator);
            rotation = new GunnerRotation(container.transform);
            
            shootPoint = container.ShootPoint;
        }
        
        public void SetFire(bool value)
        {
            animation.SetFire(value);
        }

        public void SetRun(bool value)
        {
            animation.SetRun(value);
        }

        public void SetRight()
        {
            rotation.TurnRight();
        }
        
        public void SetLeft()
        {
            rotation.TurnLeft();
        }
    }
}