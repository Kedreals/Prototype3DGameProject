using Prototype.GameObjects;
using Prototype.Handler;
using SharpDX;
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

        public Game()
        {
            win = new Window(1280, 720, "Prototype");
            gObj = new GameObject();
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
            //To DO:
        }

        private void Draw()
        {
            win.Clear(clearColor);

            win.Draw(gObj);

            win.Display();
        }
    }
}
