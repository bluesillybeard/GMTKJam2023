using System.Numerics;
using Raylib_CsLo;

public sealed class WinGameScreen : IScene
{

    EntityManager entities;
    Camera2D camera;
    public WinGameScreen(EntityManager m, Camera2D cam)
    {
        entities = m;
        camera = cam;
    }
    static Rectangle exitButtonBox = new Rectangle(30, 30, 135, 100);
    public void Frame()
    {
        Raylib.BeginMode2D(camera);
        var backgroundTexture = AssetManager.GetTexture("background.png");
        Raylib.DrawTexturePro(backgroundTexture, new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height), new Rectangle(-20000, -20000, 40000, 40000), Vector2.Zero, 0, Raylib.WHITE);
        entities.Draw();
        Raylib.EndMode2D();
        Raylib.DrawText("You Win", Raylib.GetRenderWidth()/2, Raylib.GetRenderHeight()/2, 80, Raylib.PINK);
        var buttonsTexture = AssetManager.GetTexture("buttons.png");
        Rectangle exitButton = new Rectangle(50, 157, 135, 100);
        Rectangle exitButtonNewBox = exitButtonBox;
        if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), exitButtonBox))
        {
            exitButtonNewBox = new Rectangle(exitButtonNewBox.X - 10, exitButtonNewBox.Y - 10, exitButtonNewBox.width+20, exitButtonNewBox.height+20);
        }
        Raylib.DrawTexturePro(buttonsTexture, exitButton, exitButtonNewBox, Vector2.Zero, 0, Raylib.WHITE);
    }

    public IScene Update()
    {
        //Don't update entities (for now)
        if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), exitButtonBox) && Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            return new MainMenuScreen();
        }
        return this;
    }

    public void Dispose()
    {

    }
}