using UnityEngine;

namespace ZombieShooter.Gunner
{
    public class GunnerAnimation
    {
        private static readonly int Fire = Animator.StringToHash("Fire");
        private static readonly int Run = Animator.StringToHash("Run");

        private Animator animator;

        public GunnerAnimation(Animator animator)
        {
            this.animator = animator;
        }
        
        public void SetFire(bool fire)
        {
            animator.SetBool(Fire, fire);
        }

        public void SetRun(bool run)
        {
            animator.SetBool(Run, run);
        }
    }
}