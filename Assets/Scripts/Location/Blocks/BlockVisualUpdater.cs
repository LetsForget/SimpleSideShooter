using System.Collections.Generic;
using Common.Pool;
using UnityEngine;

namespace ZombieShooter.Location
{
    public class BlockVisualUpdater
    {
        private Pool<Block> blocksPool;
        private LinkedList<Block> blocks;

        private float border;
        private Transform blocksHolder;
        
        public BlockVisualUpdater(Transform blocksHolder, Block[] blocksOriginals, float border, int blocksOrder)
        {
            this.border = border;
            this.blocksHolder = blocksHolder;
            
            var blocksList = new List<Block>();

            foreach (var block in blocksOriginals)
            {
                var instance = Object.Instantiate(block, blocksHolder);
                instance.transform.localPosition = Vector3.zero;
                instance.SetOrder(blocksOrder);
                
                blocksList.Add(instance);
            }

            blocksPool = new Pool<Block>(blocksList);
            blocks = new LinkedList<Block>();

            var startBlock = blocksPool.GetObject();
            blocks.AddFirst(startBlock);
            
            UpdateBlocks();
        }
        
        public void UpdateBlocks()
        {
            var firstBlock = blocks.First.Value;

            if (firstBlock.Position + firstBlock.Width < -border)
            {
                blocksPool.FreeObject(firstBlock);
                blocks.Remove(firstBlock);
            }
            else if (firstBlock.Position - firstBlock.Width > -border)
            {
                var block = blocksPool.GetObject();
                block.Position = firstBlock.Position - firstBlock.Width / 2 - block.Width / 2;
                blocks.AddFirst(block);
            }
            
            var lastBlock = blocks.Last.Value;
            
            if (lastBlock.Position - lastBlock.Width > border)
            {
                blocksPool.FreeObject(lastBlock);
                blocks.Remove(lastBlock);
            }
            else if (lastBlock.Position + lastBlock.Width < border)
            {
                var block = blocksPool.GetObject();
                block.Position = lastBlock.Position + lastBlock.Width / 2 + block.Width / 2;
                
                blocks.AddLast(block);
            }
        }
    }
}