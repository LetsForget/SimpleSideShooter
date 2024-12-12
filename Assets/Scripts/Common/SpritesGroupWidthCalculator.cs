using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class SpritesGroupWidthCalculator
    {
        public static float Calculate(IEnumerable<SpriteRenderer> spriteRenderers)
        {
            var minX = float.MaxValue;
            var maxX = float.MinValue;

            foreach (var spriteRenderer in spriteRenderers)
            {
                var position = spriteRenderer.transform.position;
                var spriteSize = spriteRenderer.sprite.bounds.size;
                var scale = spriteRenderer.transform.localScale;

                var halfWidth = spriteSize.x * scale.x / 2;

                var left = position.x - halfWidth;
                var right = position.x + halfWidth;
                
                minX = Mathf.Min(minX, left);
                maxX = Mathf.Max(maxX, right);
            }

            return maxX - minX; 
        }
    }
}