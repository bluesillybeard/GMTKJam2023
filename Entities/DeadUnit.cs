using System.Reflection;
using Raylib_CsLo;

public sealed class DeadUnit : IEntity
{

    public DeadUnit(Texture t, Rectangle ta, Rectangle da)
    {
        texture=t;
        textureArea = ta;
        drawArea = da;
    }
    Texture texture;
    Rectangle textureArea;
    Rectangle drawArea;
    int step;
    public void Step(EntityManager m)
    {
        step++;
        if(step >  60*60)
        {
            m.RemoveEntity(this);
        }
    }

    public void Draw()
    {
        Raylib.DrawTexturePro(texture, textureArea, drawArea, new System.Numerics.Vector2(0, 0), 0, Raylib.WHITE);
    }
}