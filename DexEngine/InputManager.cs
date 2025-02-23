using OpenTK.Windowing.GraphicsLibraryFramework;

namespace DexEngine
{
    public static class InputManager
    {
        public static bool IsKeyboardKeyPressed(Keys key)
        {
            return Dex.Engine.Window?.IsKeyPressed(key) ?? false;
        }
    }
}
