using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.GameObjects
{
    class GameObject : IDrawable
    {
        Vertex[] vertecies;

        public GameObject()
        {
            vertecies = new Vertex[] { new Vertex(new Vector3(0, 0, 1), Color4.Black), new Vertex(new Vector3(-1, 1, 1), Color4.Black), new Vertex(new Vector3(1, 1, 1), Color4.Black) };
        }

        public Vertex[] GetTriangleList()
        {
            return vertecies;
        }
    }
}
