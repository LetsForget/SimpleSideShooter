using System;
using UnityEngine;

namespace ZombieShooter.Location
{
    [Serializable]
    public struct BlocksRendererLayer
    {
        public SpriteRenderer[] renderers;
        
        public void SetOrder(int order)
        {
            foreach (var renderer in renderers)
            {
                renderer.sortingOrder = order;
            }
        }
    }
}