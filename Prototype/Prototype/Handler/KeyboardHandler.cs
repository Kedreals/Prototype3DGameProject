using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Handler
{
    /// <summary>
    /// To Do:
    /// </summary>
    class KeyboardHandler
    {
        Keyboard keyboard;
        bool[] _isPressed;
        bool[] _wasPressed;

        public KeyboardHandler()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Instantiate the joystick
            keyboard = new Keyboard(directInput);

            // Acquire the joystick
            keyboard.Properties.BufferSize = 256;
            keyboard.Acquire();
            _isPressed = new bool[keyboard.Properties.BufferSize];
            _wasPressed = new bool[_isPressed.Length];
        }

        public bool IsPressed(Key key)
        {
            return _isPressed[(int)key];
        }

        public bool OnPress(Key key)
        {
            return !_wasPressed[(int)key] && _isPressed[(int)key];
        }

        public bool OnRelease(Key key)
        {
            return _wasPressed[(int)key] && !_isPressed[(int)key];
        }

        public void Update()
        {
            var test = keyboard.GetBufferedData();

            for(int i = 0; i<_isPressed.Length; ++i)
            {
                if (_isPressed[i] != _wasPressed[i])
                    _wasPressed[i] = _isPressed[i];
            }

            foreach(var v in test)
            {
                _isPressed[(int)v.Key] = v.IsPressed;
            }
        }

    }
}
