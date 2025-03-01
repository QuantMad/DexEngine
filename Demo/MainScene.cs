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

        private int hVertexArrayObject;
        private int hVertexBufferObject;

        private Shader Shader;

        public MainScene(string name) : base(name)
        {
            Shader = new Shader("shader.vert", "shader.frag");
        }

        protected override void OnLoad(object? sender, EventArgs args)
        {
            GL.ClearColor(Color4.Aqua);
            hVertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(hVertexArrayObject);
            hVertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, hVertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }

        protected override void OnRender(object? sender, double elapsedSeconds)
        {
            Shader.Use();
            GL.BindVertexArray(hVertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);
        }

        protected override void OnUnload(object? sender, EventArgs args)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(hVertexBufferObject);
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
