using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter.Location
{
    public class BlockLayerEnumerator : IEnumerator<SpriteRenderer>
    {
        private int currentLayer;
        private int currentRenderer;
        
        private BlocksLayerConfig config;
   
        SpriteRenderer IEnumerator<SpriteRenderer>.Current => config.layers[currentLayer].renderers[currentRenderer];
        public object Current => config.layers[currentLayer].renderers[currentRenderer];
        
        public BlockLayerEnumerator(BlocksLayerConfig config)
        {
            this.config = config;
            
            Reset();
        }
        
        public bool MoveNext()
        {
            if (config.layers.Length == 0)
            {
                return false;
            }

            if (currentRenderer + 1 < config.layers[currentLayer].renderers.Length)
            {
                currentRenderer++;
                return true;
            }

            while (++currentLayer < config.layers.Length)
            {
                if (config.layers[currentLayer].renderers != null && config.layers[currentLayer].renderers.Length > 0)
                {
                    currentRenderer = 0;
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            currentLayer = 0;
            currentRenderer = -1;
        }
        

        public void Dispose() { }
    }
}