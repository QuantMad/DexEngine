using OpenTK.Graphics.OpenGL4;

namespace DexEngine
{
    public class Shader : IDisposable
    {
        private int Handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            string sourceVertex = File.ReadAllText(vertexPath);
            int hVert = CompileComponent(sourceVertex, ShaderType.VertexShader);

            string sourceFragment = File.ReadAllText(fragmentPath);
            int hFrag = CompileComponent(sourceFragment, ShaderType.FragmentShader);

            Handle = LinkProgram(hVert, hFrag);
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private int CompileComponent(string source, ShaderType type)
        {
            int handle = GL.CreateShader(type);
            GL.ShaderSource(handle, source);
            GL.CompileShader(handle);

            GL.GetShader(handle, ShaderParameter.CompileStatus, out int code);

            if (code == 0)
            {
                string info = GL.GetShaderInfoLog(handle);
                throw new ApplicationException(info);
            }

            return handle;
        }

        private int LinkProgram(int vertexHandle, int fragmentHandle)
        {
            int handle = GL.CreateProgram();
            GL.AttachShader(handle, vertexHandle);
            GL.AttachShader(handle, fragmentHandle);
            GL.LinkProgram(handle);

            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int code);
            if (code == 0)
            {
                string info = GL.GetProgramInfoLog(handle);
                throw new ApplicationException(info);
            }
            GL.DetachShader(handle, vertexHandle);
            GL.DetachShader(handle, fragmentHandle);
            GL.DeleteShader(vertexHandle);
            GL.DeleteShader(fragmentHandle);
            return handle;
        }
    }
}
