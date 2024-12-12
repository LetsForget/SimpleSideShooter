using UnityEngine;

namespace ZombieShooter.Guns
{
    public class Bullet2D : BaseBullet
    {
        [SerializeField] private float speed;
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private Collider collider;
        
        private Vector3 direction;
        
        public float Position
        {
            get => transform.position.x;
            set => transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        public int Order
        {
            get => renderer.sortingOrder;
            set => renderer.sortingOrder = value;
        }

        #region IPoolable

        public override void OnTakenFromPool()
        {
            renderer.enabled = true;
            collider.enabled = true;
        }

        public override void OnTakenBackToPool()
        {
            renderer.enabled = false;
            collider.enabled = false;
        }


        #endregion

        #region BaseBullet
        
        public override void Shoot(IShootPoint shootPoint)
        {
            var shootPosition = shootPoint.Position;
            shootPosition.z = transform.position.z;
            
            transform.position = shootPosition;
            direction = shootPoint.Direction;
        }

        public override void Move(float deltaTime)
        {
            Position += direction.x * speed * deltaTime;
        }

        public override bool CheckOutOfBorder(float border)
        {
            return Mathf.Abs(Position) > border;
        }

        public override void SetOrder(int order)
        {
            Order = order;
        }

        #endregion
    }
}