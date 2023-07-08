//the sewage factory you;re trying to protect
using System.Numerics;
using Raylib_CsLo;

public sealed class SewageFactory : IEntity
{
    public Rectangle pos = new Rectangle(-600, -300, 1200, 600);
    Rectangle source = new Rectangle(0, 0, 1200, 600);
    public void Draw()
    {
        Raylib.DrawTexturePro(AssetManager.GetTexture("FlySwatter-SewagePlant.png"), source, pos, Vector2.Zero, 0, Raylib.WHITE);
    }

    public void Step(EntityManager e)
    {

    }
}