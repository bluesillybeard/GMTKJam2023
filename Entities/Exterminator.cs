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
        bool b2, b3;
        int iterations = 0;
        do
        {
            targetPos = speed * 60*10*Vector2.Normalize(new Vector2(Random.Shared.NextSingle()-0.5f, Random.Shared.NextSingle()-0.5f));
            targetPos += pos;

            
            b2 = Vector2.Distance(targetPos, factoryPos) < 3000;
            b3 = Vector2.Distance(targetPos, factoryPos) > 5000;
            iterations++;
            if(iterations > 100)
            {
                targetPos = Vector2.Zero;
                break;
            }
        }while(b2 || b3);
    }
    public void Step(EntityManager m)
    {
        if(Random.Shared.NextSingle() < 0.005f)
        {
            var player = m.FindEntity<Player>();
            var playerPos = player?.pos ?? Vector2.Zero;
            var distance = Vector2.Distance(playerPos, this.pos);
            if(distance < 2000)
            {
                var random = (int)Random.Shared.NextSingle() * SwatUnit.sounds.Length;
                Raylib.SetSoundVolume(AssetManager.GetSound(SwatUnit.sounds[random]), 100000 / (distance*distance));
                Raylib.PlaySound(AssetManager.GetSound(SwatUnit.sounds[random]));
            }
            
        }
        if(health <= 0)
        {
            m.RemoveEntity(this);
            for(int i=0; i<flies; i++)
            {
                m.AddEntity(new HelperFly(this.pos));
            }
            m.AddEntityFirst(new DeadUnit(AssetManager.GetTexture("FlySwatter-HumanExterminatorSpriteSheet.png"), new Rectangle(53, 1, 20, 18) ,new Rectangle(pos.X, pos.Y, size, size)));
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