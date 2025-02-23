using Demo;
using OpenTK.Mathematics;
using static DexEngine.Dex;

Console.WriteLine("Hello, World!");
Vector2i windowSize = new(1024, 768);
Engine.CreateWindow(windowSize, "Lind.Engine Demo", 144);
Engine.Scenes.Add(new MainScene("MainScene"));
Engine.Scenes.Select("MainScene");
Engine.Run();