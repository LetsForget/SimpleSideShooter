using UnityEngine;

namespace ZombieShooter.Inputs
{
    public class KeyboardInput : IInput
    {
        private KeyboardInputSettings settings;

        public KeyboardInput(KeyboardInputSettings settings)
        {
            this.settings = settings;
        }
        
        public InputContainer GetInput()
        {
            var fire = Input.GetKey(settings.FireButton);
            var left = Input.GetKey(settings.LeftButton);
            var right = Input.GetKey(settings.RightButton);

            if (left && right)
            {
                left = right = false;
            }
            
            return new InputContainer
            {
                FirePressed = fire,
                LeftPressed = left,
                RightPressed = right
            };
        }
    }
}