using OpenTK.Mathematics;

namespace DexEngine
{
    public abstract class Scene(string name)
    {
        public readonly string Name = name;
        public bool IsLoaded { get; protected set; }

        private event EventHandler? LoadEvent;
        private event EventHandler? UnloadEvent;
        private event EventHandler<Vector2i>? WindowResizeEvent;
        private event EventHandler<double>? UpdateEvent;
        private event EventHandler<double>? RenderEvent;

        internal void Load()
        {
            if (IsLoaded) return;

            LoadEvent += OnLoad;
            WindowResizeEvent += OnWindowResize;
            UpdateEvent += OnUpdate;
            RenderEvent += OnRender;
            UnloadEvent += OnUnload;

            LoadEvent?.Invoke(this, EventArgs.Empty);

            IsLoaded = true;
        }

        internal void WindowResize(Vector2i size)
        {
            if (!IsLoaded) return;

            WindowResizeEvent?.Invoke(this, size);
        }

        internal void Update(double elapsedSeconds)
        {
            if (!IsLoaded) return;

            UpdateEvent?.Invoke(this, elapsedSeconds);
        }

        internal void Render(double elapsedSeconds)
        {
            if (!IsLoaded) return;

            RenderEvent?.Invoke(this, elapsedSeconds);
        }

        internal void Unload()
        {
            if (!IsLoaded) return;

            LoadEvent -= OnLoad;
            WindowResizeEvent -= OnWindowResize;
            UpdateEvent -= OnUpdate;
            RenderEvent -= OnRender;

            UnloadEvent?.Invoke(this, EventArgs.Empty);

            UnloadEvent -= OnUnload;

            IsLoaded = false;
        }

        protected abstract void OnLoad(object? sender, EventArgs args);
        protected abstract void OnWindowResize(object? sender, Vector2i size);
        protected abstract void OnUpdate(object? sender, double elapsedSeconds);
        protected abstract void OnRender(object? sender, double elapsedSeconds);
        protected abstract void OnUnload(object? sender, EventArgs args);
    }
}
