using Common;
using Common.Pool;
using UnityEngine;

namespace ZombieShooter.Location
{
    public class Block : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameObject holder;
        [SerializeField] private BlocksLayerConfig layerConfig;
        
        [SerializeField] private float _width = -1;
        public float Width
        {
            get
            {
                if (_width < 0)
                {
                    _width = SpritesGroupWidthCalculator.Calculate(layerConfig);
                }

                return _width;
            }
        }

        public float Position
        {
            get => transform.position.x;
            set => transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        
        public int LayersCount => layerConfig.layers.Length;
        
        public void OnTakenFromPool()
        {
            holder.SetActive(true);
        }

        public void OnTakenBackToPool()
        {
            holder.SetActive(false);
        }

        public void SetOrder(int order)
        {
            foreach (var layer in layerConfig.layers)
            {
                layer.SetOrder(order);
                order += 1;
            }
        }
    }
}