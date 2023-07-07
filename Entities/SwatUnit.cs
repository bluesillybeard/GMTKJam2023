using System.Data;
using System.Numerics;
using Raylib_CsLo;

public sealed class SwatUnit : IEntity
{
    int step;
    static readonly Rectangle Anim1Rect = new Rectangle(1, 1, 9, 9);
    static readonly Rectangle Anim2Rect = new Rectangle(11, 1, 9, 9);
    static readonly Rectangle Anim3Rect = new Rectangle(23, 1, 9, 9);
    public void Draw()
    {
        //TODO: animation
        var texture = AssetManager.GetTexture("FlySwatter-HumanArmySpriteSheet.png");
        Rectangle anim;
        
        Raylib.DrawTexturePro(texture, Anim1Rect, new Rectangle(0, 0, 50, 50), Vector2.Zero, 0, Raylib.WHITE);
    }

    public void Step(EntityManager m)
    {
        step++;
        
    }
}