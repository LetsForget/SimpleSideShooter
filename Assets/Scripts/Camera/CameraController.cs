using UnityEngine;

namespace ZombieShooter.Cameras
{
    public class CameraController
    {
        private Camera camera;
        private CameraConfig config;
        
        public CameraController(Camera camera, CameraConfig config)
        {
            this.camera = camera;
            this.config = config;
            
            var camTransform = camera.transform;
            camTransform.position = config.Position;
            camTransform.rotation = Quaternion.Euler(config.Rotation);
            
            this.camera.orthographicSize = config.Size;
        }
    }
}