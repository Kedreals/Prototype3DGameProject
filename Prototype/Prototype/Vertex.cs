using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    struct Vertex
    {
        public Vector4 Position;
        public Color4 Color;

        public static readonly InputElement[] InputElemets = new InputElement[]
        { new InputElement("POSITION", 0, Format.R32G32B32A32_Float,0, 0),
            new InputElement("COLOR", 0, Format.R32G32B32A32_Float,Utilities.SizeOf<Vector4>() ,0)};

        public Vertex(Vector3 pos, Color4 color)
        {
            Position = new Vector4(pos,1);
            Color = color;
        }

        public void Transform(Matrix m)
        {
            Position = Vector4.Transform(Position, m);
        }
    }
}
