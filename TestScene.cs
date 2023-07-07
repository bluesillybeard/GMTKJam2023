using Raylib_CsLo;

public sealed class TestScene : IScene
{
    Camera2D camera = new Camera2D();
    public IScene Frame()
    {
        var texture = AssetManager.GetTexture("dicetest.png");
        Raylib.BeginMode2D(camera);
        //Raylib.DrawTextureRec(texture, new Rectangle(0, 0, 50, 50), new System.Numerics.Vector2(10, 10), Raylib.WHITE);
        Raylib.DrawTexturePro(texture, new Rectangle(0, 0, texture.width, texture.height), new Rectangle(0, 0, 50, 50), new System.Numerics.Vector2(25, 25), 0, Raylib.WHITE);
        Raylib.EndMode2D();
        return this;
    }

    public void PreFrame()
    {
        if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            Raylib.PlaySound(AssetManager.GetSound("snap.ogg"));
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            camera.target -= new System.Numerics.Vector2(0, 1);
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            camera.target -= new System.Numerics.Vector2(0, -1);
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            camera.target -= new System.Numerics.Vector2(-1, 0);
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            camera.target -= new System.Numerics.Vector2(1, 0);
        }
        camera.zoom = 1;
    }


    public void Dispose()
    {

    }
}