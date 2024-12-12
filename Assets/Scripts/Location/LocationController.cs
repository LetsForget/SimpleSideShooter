using Common;
using UnityEngine;
using ZombieShooter.Gunner;

namespace ZombieShooter.Location
{
    public class LocationController
    {
        private LocationData locationData;
        
        public LocationController(LocationData locationData)
        {
            this.locationData = locationData;
        }

        public GunnerContainer SpawnCharacter(GunnerContainer container)
        {
            var result = GameObject.Instantiate(container, locationData.CharacterHolder);
            result.SpriteRenderer.sortingOrder = locationData.CharacterOrder;
            result.transform.ResetLocal();
            
            return result;
        }
        
        public void Move(float delta)
        {
            locationData.MovableHolder.position += Vector3.right * delta;

            locationData.UpdateBlocks();
        }
    }
}