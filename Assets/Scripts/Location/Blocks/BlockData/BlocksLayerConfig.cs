using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter.Location
{
    [Serializable]
    public struct BlocksLayerConfig : IEnumerable<SpriteRenderer>
    {
         public BlocksRendererLayer[] layers;

         IEnumerator<SpriteRenderer> IEnumerable<SpriteRenderer>.GetEnumerator()
         {
             return new BlockLayerEnumerator(this);
         }

         public IEnumerator GetEnumerator()
         {
             return new BlockLayerEnumerator(this);
         }
    }
}