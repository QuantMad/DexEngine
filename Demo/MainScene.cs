using DexEngine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static DexEngine.Dex;

namespace Demo
{
    class MainScene : Scene
    {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
             0.5f, -0.5f, 0.0f, //Bottom-right vertex
             0.0f,  0.5f, 0.0f  //Top vertex
        };
        int VertexBufferObject;

        public MainScene(string name) : base(name)
        {

        }

        protected override void OnLoad(object? sender, EventArgs args)
        {

        }

        protected override void OnRender(object? sender, double elapsedSeconds)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        }

        protected override void OnUnload(object? sender, EventArgs args)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }

        protected override void OnUpdate(object? sender, double elapsedSeconds)
        {
            if (InputManager.IsKeyboardKeyPressed(Keys.D1))
            {
                Console.WriteLine($"this is a \"{Name}\" scene");
            }
        }

        protected override void OnWindowResize(object? sender, Vector2i size)
        {
        }
    }
}
