using Prototype.GameObjects;
using Prototype.Handler;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.GameStates
{
    class Game
    {
        Window win;
        Color4 clearColor = new Color4(113f/255, 75f/255, 3f/255, 1);

        GameObject gObj;
        KeyboardHandler keyboard;
        Camara c;

        public Game()
        {
            win = new Window(1280, 720, "Prototype");
            c = win.GetCamara();
            gObj = new GameObject();

            keyboard = new KeyboardHandler();
        }

        public void Run()
        {
            RenderLoop.Run(win.Form, RenderCallback);
        }

        private void RenderCallback()
        {
            Update();
            Draw();
        }

        private void Update()
        {
            keyboard.Update();

            if (keyboard.IsPressed(Key.W))
                c.Move(new Vector3(0, 0.01f, 0));

            if (keyboard.IsPressed(Key.S))
                c.Move(new Vector3(0, -0.01f, 0));

            if (keyboard.IsPressed(Key.D))
                c.Move(new Vector3(0.01f, 0, 0));

            if (keyboard.IsPressed(Key.A))
                c.Move(new Vector3(-0.01f, 0, 0));

            if (keyboard.IsPressed(Key.Q))
                c.Move(new Vector3(0, 0, 0.01f));

            if (keyboard.IsPressed(Key.E))
                c.Move(new Vector3(0, 0, -0.01f));

            if (keyboard.IsPressed(Key.R))
                c.Rotate(Camara.Achse.X, 0.1f);
            if (keyboard.IsPressed(Key.F))
                c.Rotate(Camara.Achse.X, -0.1f);

            if (keyboard.IsPressed(Key.Left))
                c.Rotate(Camara.Achse.Y, 0.1f);

            if (keyboard.IsPressed(Key.Right))
                c.Rotate(Camara.Achse.Y, -0.1f);
        }

        private void Draw()
        {
            win.Clear(clearColor);

            win.Draw(gObj);

            win.Display();
        }
    }
}
