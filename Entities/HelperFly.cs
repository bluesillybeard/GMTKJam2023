using System.Net.Http.Headers;
using System.Numerics;
using System.Windows.Markup;
using Raylib_CsLo;

public sealed class HelperFly : IEntity
{
    const int speed = 10;
    public const int size = 20;
    public Vector2 pos;
    float angle;
    Vector2 targetPos;
    public HelperFly(Vector2 pos)
    {
        this.pos = pos;
        //This is so that swats spawned in the same frame don't walk in sync
        step = Random.Shared.Next() % 60;
    }
    int step;
    public void Draw()
    {
        var texture = AssetManager.GetTexture("FlySwatter-FlyFriendSprite.png");
        Raylib.DrawTexturePro(texture, new Rectangle(0, 0, texture.width, texture.height), new Rectangle(pos.X, pos.Y, size, size), new Vector2(size/2, size/2), angle, Raylib.WHITE);
    }

    private void MakeNewTargetPos(EntityManager m)
    {
        Vector2 nearestPos = new Vector2(float.PositiveInfinity, 0);
        foreach(var e in m.Entities)
        {
            //attack swat units
            if(e is SwatUnit u)
            {
                if(Vector2.Distance(nearestPos, this.pos) > Vector2.Distance(u.pos, this.pos))
                {
                    nearestPos = u.pos;
                }
            }
        }

        targetPos = nearestPos;
    }
    public void Step(EntityManager m)
    {
        step++;
        var factory = m.FindEntity<SewageFactory>();
        //Move randomly
        var factoryRect = factory?.pos ?? new Rectangle(-20, -20, 20, 20);
        var factoryPos = new Vector2(factoryRect.X + factoryRect.width/2, factoryRect.Y + factoryRect.height/2);
        var factoryRectExpanded = new Rectangle(factoryRect.x-size/2, factoryRect.y-size/2, factoryRect.width+size, factoryRect.height+size);
        if(step % 10 == 0)
        {
            MakeNewTargetPos(m);
            if(targetPos == new Vector2(float.PositiveInfinity, 0))
            {
                targetPos = factoryPos + new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle()) * 300;
            }
            targetPos += new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle())*20;
        }
        
        Vector2 moveDirection = targetPos - pos;
        moveDirection = Vector2.Normalize(moveDirection);
        var newPos = pos + moveDirection*speed;
        pos = newPos;
        angle = MathF.Atan2(moveDirection.X, -moveDirection.Y) * 180f/MathF.PI;

        foreach(var entity in m.Entities)
        {
            if(entity is SwatUnit swat && (pos - swat.pos).Length() <= (size + SwatUnit.size)/2)
            {
                swat.TakeDamage(1);
            }
            // if(entity is Exterminator exterminator && (pos - exterminator.pos).Length() <= (size + SwatUnit.size)/2)
            // {
            //     exterminator.TakeDamage(100);
            // }
        }

    }
}