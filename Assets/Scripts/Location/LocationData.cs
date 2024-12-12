using UnityEngine;

namespace ZombieShooter.Location
{
    public struct LocationData
    {
        public Block Background;
        public Block Floor;

        public Transform CharacterHolder;
        public int CharacterOrder;
        
        public Transform MovableHolder;
        
        public Transform EnemiesHolder;
        public int EnemiesOrder;
        
        public BlockVisualUpdater BackgroundUpdater;
        public BlockVisualUpdater MidgroundUpdater;
        public BlockVisualUpdater ForegroundUpdater;

        public void UpdateBlocks()
        {
            BackgroundUpdater.UpdateBlocks();
            MidgroundUpdater.UpdateBlocks();
            ForegroundUpdater.UpdateBlocks();
        }
    }
}