using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Handler
{
    class Camara
    {
        Matrix InvertedPos;
        Matrix Translation;
        Matrix RotationX;
        Matrix RotationY;
        Matrix RotationZ;

        public Camara()
        {
            InvertedPos = new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0, 
                0, 0, 0, 1);
        }

        public void Move(Vector3 move)
        {
            Translation = new Matrix(
                1, 0, 0, move.X,
                0, 1, 0, move.Y,
                0, 0, 1, move.Z,
                0, 0, 0, 1
                );
        }

        public Matrix GetInvertedCamaraPosition()
        {
            return InvertedPos;
        }
    }
}
