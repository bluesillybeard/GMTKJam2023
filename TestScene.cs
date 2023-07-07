using Raylib_CsLo;

public sealed class TestScene : IScene
{
    public IScene Frame()
    {
        var texture = AssetManager.GetTexture("dicetest.png");
        Raylib.DrawTexture(texture, 0, 0, Raylib.WHITE);
        return this;
    }

    public void PreFrame()
    {
        if(Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            Raylib.PlaySound(AssetManager.GetSound("snap.ogg"));
        }
    }

    public void Dispose()
    {

    }
}