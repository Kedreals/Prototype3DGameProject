using SharpDX;
using D3D11 = SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.D3DCompiler;
using System.Drawing;
using Prototype.Utility;

namespace Prototype.Handler
{
    /// <summary>
    /// To Do:
    ///     Draw
    ///     InitializeDeviceRessources
    ///     InitializeShader
    /// </summary>
    class Window : IDisposable
    {
        /// <summary>
        /// The Window shown on the Screen
        /// </summary>
        public RenderForm Form { get; private set; }

        /// <summary>
        /// The Size of this Window
        /// </summary>
        public Vector2i Size { get; private set; }

        /// <summary>
        /// the device, that draws to the Form
        /// </summary>
        private D3D11.Device device;
        /// <summary>
        /// the Context of the device, simply put the commands for it
        /// </summary>
        private D3D11.DeviceContext deviceContext;
        /// <summary>
        /// simply put the struct that manages the swaping between the data we wanna draw and that what is on the screen right now
        /// </summary>
        private SwapChain swapChain;

        /// <summary>
        /// the part of the world we will see
        /// </summary>
        private D3D11.RenderTargetView view;

        /// <summary>
        /// a buffer for vertecies
        /// </summary>
        private D3D11.Buffer vertexBuffer;
        /// <summary>
        /// a buffer for constants
        /// </summary>
        private D3D11.Buffer constantBuffer;

        /// <summary>
        /// a vertexShader
        /// </summary>
        private D3D11.VertexShader vertexShader;
        /// <summary>
        /// a pixelShader
        /// </summary>
        private D3D11.PixelShader pixelShader;

        /// <summary>
        /// the Input Elements for the VertexShader
        /// </summary>
        private D3D11.InputElement[] inputElements = Vertex.InputElemets;

        /// <summary>
        /// How the VertexShader handles Input
        /// </summary>
        private ShaderSignature inputSignature;
        /// <summary>
        /// How the layout of the Shader looks
        /// </summary>
        private D3D11.InputLayout inputLayout;

        /// <summary>
        /// defines how much of the screen is drawn from one device
        /// </summary>
        private Viewport viewport;

        public Window(int Width, int Height, string title)
        {
            Form = new RenderForm(title);
            Form.ClientSize = new Size(Width, Height);
            Form.AllowUserResizing = false;
            Size = new Vector2i(Width, Height);

            InitializeDeviceResources();
            InitializeShader();
        }

        private void InitializeDeviceResources()
        {
            ModeDescription backBufferDesc = new ModeDescription(Size.X, Size.Y, new Rational(60,1), Format.R8G8B8A8_UInt);

            //To be completed
        }

        private void InitializeShader()
        {
             //To Do:
        }

        public void Dispose()
        {
            Form.Dispose();
            view.Dispose();
            deviceContext.Dispose();
            device.Dispose();
            swapChain.Dispose();
            vertexBuffer.Dispose();
            constantBuffer.Dispose();
            vertexShader.Dispose();
            pixelShader.Dispose();
            inputLayout.Dispose();
            inputSignature.Dispose();
        }
    }
}
