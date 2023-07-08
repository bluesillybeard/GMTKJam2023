using System.Numerics;
using Raylib_CsLo;

public sealed class Exterminator : IEntity
{
    const int speed = 2;
    public const int size = 50;
    public Vector2 pos;
    float angle;
    float health;
    Vector2 targetPos;
    public Exterminator(Vector2 pos)
    {
        this.pos = pos;
        //This is so that exterminators spawned in the same frame don't walk in sync
        step = Random.Shared.Next() % 60;
        health = 100;
        MakeNewTargetPos(new Rectangle(0, 0, 0, 0), Vector2.Zero);
        flies = 1;
    }
    int step;
    int flies;
    static readonly Rectangle[] AnimRects = new Rectangle[]{new Rectangle(0, 1, 17, 14), new Rectangle(18, 1, 17, 14), new Rectangle(36, 1, 17, 14), new Rectangle(18, 1, 17, 14)};
    public void Draw()
    {
        var texture = AssetManager.GetTexture("FlySwatter-HumanExterminatorSpriteSheet.png");
        Rectangle anim = AnimRects[(step/10) % 4];
        Raylib.DrawTexturePro(texture, anim, new Rectangle(pos.X, pos.Y, size, size), new Vector2(size/2, size/2), angle, Raylib.WHITE);
    }

    private void MakeNewTargetPos(Rectangle factoryRectExpanded, Vector2 factoryPos)
    {
        do
        {
            targetPos = speed * 60*10*Vector2.Normalize(new Vector2(Random.Shared.NextSingle()-0.5f, Random.Shared.NextSingle()-0.5f));
            targetPos += pos;
        }while(Raylib.CheckCollisionPointRec(targetPos, factoryRectExpanded) && Vector2.Distance(targetPos, factoryPos) > 800 && Vector2.Distance(targetPos, factoryPos) < 4000);
    }
    public void Step(EntityManager m)
    {
        if(health <= 0)
        {
            m.RemoveEntity(this);
            m.AddEntity(new HelperFly(this.pos));
            return;
        }
        step++;
        var factory = m.FindEntity<SewageFactory>();
        //Move randomly
        var factoryRect = factory?.pos ?? new Rectangle(-20, -20, 20, 20);
        var factoryPos = new Vector2(factoryRect.X + factoryRect.width/2, factoryRect.Y + factoryRect.height/2);
        var factoryRectExpanded = new Rectangle(factoryRect.x-size/2, factoryRect.y-size/2, factoryRect.width+size, factoryRect.height+size);
        if(step % (60*10) == 0)
        {
            MakeNewTargetPos(factoryRectExpanded, factoryPos);
        }
        
        Vector2 moveDirection = targetPos - pos;
        moveDirection = Vector2.Normalize(moveDirection);
        var newPos = pos + moveDirection*speed;
        pos = newPos;
        angle = MathF.Atan2(moveDirection.X, -moveDirection.Y) * 180f/MathF.PI;

        foreach(var entity in m.Entities)
        {
            if(entity is HelperFly fly && (pos - fly.pos).Length() <= (size + SwatUnit.size)/2)
            {
                flies++;
                m.RemoveEntity(fly);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}