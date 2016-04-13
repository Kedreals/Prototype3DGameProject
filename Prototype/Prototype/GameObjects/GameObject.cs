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
        Quader q;

        public GameObject()
        {
            q = new Quader(2, 3, 2, Color4.White);
        }

        public Vertex[] GetTriangleList()
        {
            return q.GetTriangulation();
        }
    }
}
