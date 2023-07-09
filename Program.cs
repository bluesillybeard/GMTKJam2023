using Raylib_CsLo;

public static class Program
{
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
        //TODO: Make game not relient on FPS for gameplay
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(1440-200, 900-200, "Fly Swatter");
        Raylib.InitAudioDevice();

        IScene? scene = new MainMenuScreen();
        while(!Raylib.WindowShouldClose() && scene is not null)
        {
            //Raylib.PollInputEvents();
            AssetManager.Update();
            scene = scene.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            scene.Frame();
            Raylib.EndDrawing();
        }
        scene?.Dispose();
        AssetManager.Dispose();
        Raylib.CloseWindow();
    }
}