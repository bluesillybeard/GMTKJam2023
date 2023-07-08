using System.Data;
using System.Numerics;
using System.Security.AccessControl;
using Raylib_CsLo;

public sealed class SwatUnit : IEntity
{
    const int speed = 1;
    public const int size = 50;
    public Vector2 pos;
    float angle;
    float health;
    public SwatUnit(Vector2 pos)
    {
        this.pos = pos;
        //This is so that swats spawned in the same frame don't walk in sync
        step = Random.Shared.Next() % 60;
        health = 100;
    }
    int step;
    static readonly Rectangle[] AnimRects = new Rectangle[]{new Rectangle(0, 1, 18, 14), new Rectangle(17, 1, 18, 14), new Rectangle(36, 1, 18, 14), new Rectangle(17, 1, 17, 14)};
    public void Draw()
    {
        var texture = AssetManager.GetTexture("FlySwatter-HumanSpriteSheet.png");
        Rectangle anim = AnimRects[(step/10) % 4];
        Raylib.DrawTexturePro(texture, anim, new Rectangle(pos.X, pos.Y, size, size), new Vector2(size/2, size/2), angle, Raylib.WHITE);
    }

    public void Step(EntityManager m)
    {
        if(health <= 0)
        {
            m.RemoveEntity(this);
            return;
        }
        step++;
        var factory = m.FindEntity<SewageFactory>();
        //Move towards the factory
        var factoryPos = new Vector2(factory?.pos.X ?? 0, factory?.pos.Y ?? 0);
        var factoryRect = factory?.pos ?? new Rectangle(-20, -20, 20, 20);
        var factoryRectExpanded = new Rectangle(factoryRect.x-size/2, factoryRect.y-size/2, factoryRect.width+size, factoryRect.height+size);
        Vector2 moveDirection = factoryPos - pos;
        moveDirection = Vector2.Normalize(moveDirection);
        var newPos = pos + moveDirection*speed;
        if(!Raylib.CheckCollisionPointRec(newPos, factoryRectExpanded))
        {
            pos = newPos;
        }
        angle = MathF.Atan2(moveDirection.X, -moveDirection.Y) * 180f/MathF.PI;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}