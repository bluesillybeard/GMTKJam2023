using Raylib_CsLo;

public static class Program
{
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_VSYNC_HINT);
        //TODO: Make game not relient on FPS for gameplay
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(800, 600, "GMTK 2023");
        Raylib.InitAudioDevice();

        IScene? scene = new MainGameScene();
        while(!Raylib.WindowShouldClose() && scene is not null)
        {
            //Raylib.PollInputEvents();
            scene = scene.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            scene.Frame();
            Raylib.EndDrawing();
        }
        scene?.Dispose();
        Raylib.CloseWindow();
    }
}