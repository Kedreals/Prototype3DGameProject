using Prototype.Handler;
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

        public Game()
        {
            win = new Window(1280, 720, "Prototype");
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
            //To Do:
        }
    }
}
