using SharpDX;
using D3D11 = SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.D3DCompiler;
using System.Drawing;
using Prototype.Utility;
using SharpDX.Direct3D;

namespace Prototype.Handler
{
    /// <summary>
    /// To Do:
    ///     Draw
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
        /// a buffer for the Ratio between width and height of the window
        /// </summary>
        private D3D11.Buffer ratioBuffer;

        /// <summary>
        /// buffer for the camara position
        /// </summary>
        private D3D11.Buffer camaraBuffer;

        /// <summary>
        /// the trianglelist of the content
        /// </summary>
        private List<Vertex> TriangleList;
        /// <summary>
        /// for the shader the vertex array
        /// </summary>
        private Vertex[] VertexArray;

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

        /// <summary>
        /// the Camara of this thingy
        /// </summary>
        private Camara camara;
        /// <summary>
        /// the inverted camaraposition
        /// </summary>
        private Matrix invCamara;

        private int lastVertexArrayLength;

        public Window(int Width, int Height, string title)
        {
            Form = new RenderForm(title);
            Form.ClientSize = new Size(Width, Height);
            Form.AllowUserResizing = false;
            Size = new Vector2i(Width, Height);

            TriangleList = new List<Vertex>();

            camara = new Camara();

            InitializeDeviceResources();
            InitializeShader();
        }

        /// <summary>
        /// Gets the Camara of this window
        /// </summary>
        /// <returns>the Camara for this window</returns>
        public Camara GetCamara()
        {
            return camara;
        }

        private void InitializeDeviceResources()
        {
            ModeDescription backBufferDesc = new ModeDescription(Size.X, Size.Y, new Rational(60,1), Format.R8G8B8A8_UNorm);

            SwapChainDescription swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                IsWindowed = true,
                OutputHandle = Form.Handle
            };

            viewport = new Viewport(0, 0, Size.X, Size.Y);

            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.Debug, swapChainDesc, out device, out swapChain);

            deviceContext = device.ImmediateContext;

            using(D3D11.Texture2D backBuffer = swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                view = new D3D11.RenderTargetView(device, backBuffer);
            }

            deviceContext.OutputMerger.SetRenderTargets(view);

            deviceContext.Rasterizer.SetViewport(viewport);

            vertexBuffer = D3D11.Buffer.Create<Vertex>(device, D3D11.BindFlags.VertexBuffer, new Vertex[1]);
            lastVertexArrayLength = 1;
        }

        private void InitializeShader()
        {
            //creating shaderbytecode from a file and using it for initializing shader (data, start methode, version to use, flag)
            using (var vertextShaderByteCode = ShaderBytecode.CompileFromFile("Shader/standardVertexShader.hlsl", "main", "vs_4_0", ShaderFlags.Debug))
            {
                //get the signature for the input
                inputSignature = ShaderSignature.GetInputSignature(vertextShaderByteCode);
                //creating a vertex shader for the device
                vertexShader = new D3D11.VertexShader(device, vertextShaderByteCode);
            }
            using (var pixelShaderByteCode = ShaderBytecode.CompileFromFile("Shader/standardPixelShader.hlsl", "main", "ps_4_0", ShaderFlags.Debug))
            {
                //creating a pixel shader for the device
                pixelShader = new D3D11.PixelShader(device, pixelShaderByteCode);
            }

            //creating the Input Layout (device, signature, inputElements)
            inputLayout = new D3D11.InputLayout(device, inputSignature, inputElements);

            //set the device, to use the shader
            deviceContext.VertexShader.Set(vertexShader);
            deviceContext.PixelShader.Set(pixelShader);

            //set the primitive topology (how to treat the input)
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            //set the device to use the input Layout
            deviceContext.InputAssembler.InputLayout = inputLayout;

            deviceContext.InputAssembler.SetVertexBuffers(0, new D3D11.VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vertex>(), 0));

            float ratio = (float)Size.X / (float)Size.Y;

            Matrix ratioMatrix = new Matrix(1 / ratio, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

            ratioBuffer = D3D11.Buffer.Create<Matrix>(device, D3D11.BindFlags.ConstantBuffer, ref ratioMatrix);

            deviceContext.VertexShader.SetConstantBuffer(0, ratioBuffer);

            Matrix invCamara = camara.GetInvertedCamaraPosition();

            camaraBuffer = D3D11.Buffer.Create<Matrix>(device, D3D11.BindFlags.ConstantBuffer, ref invCamara);
            deviceContext.VertexShader.SetConstantBuffer(1, camaraBuffer);

        }

        /// <summary>
        /// clears the window with black
        /// </summary>
        public void Clear()
        {
            Clear(new Color4(0, 0, 0, 1));
        }

        /// <summary>
        /// clears the window with the given color
        /// </summary>
        /// <param name="c"></param>
        public void Clear(Color4 c)
        {
            deviceContext.ClearRenderTargetView(view, c);
            TriangleList.Clear();
        }

        /// <summary>
        /// draws the given object
        /// </summary>
        /// <param name="draw"></param>
        public void Draw(IDrawable draw)
        {
            Vertex[] arr = draw.GetTriangleList();
            foreach(Vertex v in arr)
            {
                TriangleList.Add(v);
            }
        }

        /// <summary>
        /// displays all drawn content
        /// </summary>
        public void Display()
        {
            VertexArray = TriangleList.ToArray();

            deviceContext.UpdateSubresource(VertexArray, vertexBuffer);
            invCamara = camara.GetInvertedCamaraPosition();
            deviceContext.UpdateSubresource(ref invCamara, camaraBuffer);

            if (VertexArray.Length != lastVertexArrayLength)
            {
                vertexBuffer.Dispose();
                vertexBuffer = D3D11.Buffer.Create<Vertex>(device, D3D11.BindFlags.VertexBuffer, VertexArray);
                deviceContext.InputAssembler.SetVertexBuffers(0, new D3D11.VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vertex>(), 0));
            }

            deviceContext.Draw(VertexArray.Count(), 0);

            swapChain.Present(1, PresentFlags.None);
        }

        /// <summary>
        /// frees all resources of this window
        /// </summary>
        public void Dispose()
        {
            Form.Dispose();
            view.Dispose();
            deviceContext.Dispose();
            device.Dispose();
            swapChain.Dispose();
            vertexBuffer.Dispose();
            ratioBuffer.Dispose();
            vertexShader.Dispose();
            pixelShader.Dispose();
            inputLayout.Dispose();
            inputSignature.Dispose();
        }
    }
}
