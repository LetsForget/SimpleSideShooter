using System.Linq;
using Common;
using UnityEngine;

namespace ZombieShooter.Location
{
    public class LocationFabric
    {
        public LocationData CreateLocation(LocationConfig config)
        {
            var location = new GameObject("Location");

            var movableHolder = new GameObject("Movable");
            movableHolder.transform.SetParent(location.transform, false);
            
            var staticHolder = new GameObject("Static");
            staticHolder.transform.SetParent(location.transform, false);
            
            var order = 0;
            
            var background = GameObject.Instantiate(config.MainBackground, staticHolder.transform);
            background.name = "Background";
            background.SetOrder(order);
            background.transform.ResetLocal();
            background.transform.localPosition = new Vector3(0, 0, order);
            
            order += background.LayersCount;
            
            var backgroundObjectsHolder = new GameObject("Background Objects");
            backgroundObjectsHolder.transform.SetParent(movableHolder.transform, false);
            backgroundObjectsHolder.transform.localPosition = new Vector3(0, 0, order);
            var backgroundUpdater = new BlockVisualUpdater(backgroundObjectsHolder.transform, config.BackgroundBlocks, config.BlocksBorder, order);

            order += config.BackgroundBlocks.Max(x => x.LayersCount);
            
            var midgroundObjectsHolder = new GameObject("Midground Objects");
            midgroundObjectsHolder.transform.SetParent(movableHolder.transform, false);
            midgroundObjectsHolder.transform.localPosition = new Vector3(0, 0, order);
            var midgroundUpdater = new BlockVisualUpdater(midgroundObjectsHolder.transform, config.MidgroundBlocks, config.BlocksBorder, order);

            order += config.MidgroundBlocks.Max(x => x.LayersCount);
            
            var floor = GameObject.Instantiate(config.Floor, staticHolder.transform);
            floor.name = "Floor";
            floor.SetOrder(order);
            floor.transform.ResetLocal();
            floor.transform.localPosition = new Vector3(0, 0, order);
            
            order += floor.LayersCount;
            
            var characterHolder = new GameObject("Character");
            characterHolder.transform.SetParent(staticHolder.transform, false);
            characterHolder.transform.localPosition = new Vector3(0, config.CharactersGroundLevel, order);
            var characterOrder = order;
            
            order += 1;
            
            var enemiesHolder = new GameObject("Enemies");
            enemiesHolder.transform.SetParent(movableHolder.transform, false);
            enemiesHolder.transform.localPosition = new Vector3(0, config.CharactersGroundLevel, order);
            var enemiesOrder = order;
            
            order += 1;
            
            var foregroundObjectsHolder = new GameObject("Foreground Objects");
            foregroundObjectsHolder.transform.SetParent(movableHolder.transform, false);
            foregroundObjectsHolder.transform.localPosition = new Vector3(0, 0, order);
            var foregroundUpdater = new BlockVisualUpdater(foregroundObjectsHolder.transform, config.ForegroundBlocks, config.BlocksBorder, order);

            order += config.ForegroundBlocks.Max(x => x.LayersCount);
            
            return new LocationData
            {
                Background = background,
                Floor = floor,
                
                CharacterHolder = characterHolder.transform,
                CharacterOrder = characterOrder,
                
                MovableHolder = movableHolder.transform,
                
                EnemiesHolder = enemiesHolder.transform,
                EnemiesOrder = enemiesOrder,
                
                BackgroundUpdater = backgroundUpdater,
                MidgroundUpdater = midgroundUpdater,
                ForegroundUpdater = foregroundUpdater
            };
        }
    }
}