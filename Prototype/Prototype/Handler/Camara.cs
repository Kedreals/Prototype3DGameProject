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
        public enum Achse
        {
            X,
            Y,
            Z
        }

        Matrix Translation;
        Matrix RotationX;
        Matrix RotationY;
        Matrix RotationZ;

        public Camara()
        {
            Translation = new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0, 
                0, 0, 0, 1);

            RotationX = Translation;
            RotationY = Translation;
            RotationZ = Translation;
        }

        public void Move(Vector3 move)
        {
            Translation *= new Matrix(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                -move.X, -move.Y, -move.Z, 1
                );
        }

        public void Rotate(Achse a, float _degree)
        {
            float degree = (_degree / 360) * (float)Math.PI * 2;

            switch (a)
            {
                case Achse.X:
                    RotationX *= new Matrix(
                        1, 0, 0, 0,
                        0, (float)Math.Cos(-degree), (float)-Math.Sin(-degree), 0, 
                        0, (float)Math.Sin(-degree), (float)Math.Cos(-degree), 0, 
                        0, 0, 0, 1);

                    break;
                case Achse.Y:
                    RotationY *= new Matrix(
                        (float)Math.Cos(-degree), 0, (float)Math.Sin(-degree),0,
                        0, 1, 0, 0,
                        (float)-Math.Sin(-degree), 0, (float)Math.Cos(-degree), 0,
                        0, 0, 0, 1
                        );

                    break;
                case Achse.Z:
                    RotationZ *= new Matrix(
                        (float)Math.Cos(-degree), (float)-Math.Sin(-degree), 0, 0,
                        (float)Math.Sin(-degree), (float)Math.Cos(-degree), 0, 0,
                        0, 0, 1, 0,
                        0, 0, 0, 1
                        );

                    break;
            }
        }

        public Matrix GetInvertedCamaraPosition()
        {
            return RotationZ*RotationY*RotationX*Translation;
        }
    }
}
