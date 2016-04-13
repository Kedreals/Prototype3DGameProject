using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.GameObjects
{
    class Quader : IDisposable
    {
        Vertex[] vertecies;
        List<Vertex> triangulation;

        public Quader(float sizex, float sizey, float sizez, Color4 c)
        {
            Color4 c1 = Color4.Black;
            Color4 c2 = Color4.White;

            vertecies = new Vertex[8];
            vertecies[0] = new Vertex(new Vector3(0,0,0), c1);
            vertecies[1] = new Vertex(new Vector3(sizex, 0, 0), c1);
            vertecies[2] = new Vertex(new Vector3(sizex, 0, sizez), c1);
            vertecies[3] = new Vertex(new Vector3(0, 0, sizez), c1);
            vertecies[4] = new Vertex(new Vector3(0, sizey, 0), c2);
            vertecies[5] = new Vertex(new Vector3(sizex, sizey, 0), c2);
            vertecies[6] = new Vertex(new Vector3(sizex, sizey, sizez), c2);
            vertecies[7] = new Vertex(new Vector3(0, sizey, sizez), c2);

            triangulation = new List<Vertex>();
            Triangulate();
        }

        void Triangulate()
        {
            //Vorderseite
            triangulation.Add(vertecies[0]);
            triangulation.Add(vertecies[5]);
            triangulation.Add(vertecies[1]);

            triangulation.Add(vertecies[0]);
            triangulation.Add(vertecies[4]);
            triangulation.Add(vertecies[5]);

            //unterseite
            triangulation.Add(vertecies[1]);
            triangulation.Add(vertecies[3]);
            triangulation.Add(vertecies[0]);

            triangulation.Add(vertecies[1]);
            triangulation.Add(vertecies[2]);
            triangulation.Add(vertecies[3]);

            //linke seite
            triangulation.Add(vertecies[0]);
            triangulation.Add(vertecies[3]);
            triangulation.Add(vertecies[7]);

            triangulation.Add(vertecies[0]);
            triangulation.Add(vertecies[7]);
            triangulation.Add(vertecies[4]);

            //rechte seite
            triangulation.Add(vertecies[1]);
            triangulation.Add(vertecies[5]);
            triangulation.Add(vertecies[6]);

            triangulation.Add(vertecies[1]);
            triangulation.Add(vertecies[6]);
            triangulation.Add(vertecies[2]);

            //rückseite
            triangulation.Add(vertecies[2]);
            triangulation.Add(vertecies[6]);
            triangulation.Add(vertecies[7]);

            triangulation.Add(vertecies[2]);
            triangulation.Add(vertecies[7]);
            triangulation.Add(vertecies[3]);

            //oberseite
            triangulation.Add(vertecies[4]);
            triangulation.Add(vertecies[7]);
            triangulation.Add(vertecies[6]);

            triangulation.Add(vertecies[4]);
            triangulation.Add(vertecies[6]);
            triangulation.Add(vertecies[5]);
        }

        public Vertex[] GetTriangulation()
        {
            return triangulation.ToArray();
        }

        public void Dispose()
        {
            vertecies = null;
            triangulation = null;
        }
    }
}
