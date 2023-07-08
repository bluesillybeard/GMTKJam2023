using System.Buffers;
using System.Diagnostics;
using System.Numerics;
using Raylib_CsLo;

public sealed class Player : IEntity
{
    const float speed = 10;
    const float rotationSpeed = 4;
    const int size = 80;

    static readonly Rectangle[] anims = new[]{new Rectangle(0, 0, 35, 38), new Rectangle(36, 0, 35, 38), };
    static readonly Rectangle swatting = new Rectangle(70, 0, 35, 38);
    int step = Random.Shared.Next() % 60;
    int swatAnimationCooldown = 0;
    public Vector2 pos = Vector2.Zero;
    float angle = 0;


    public void Draw()
    {
        int anim = (step/3) % 2;
        Rectangle animRect = anims[anim];
        if(swatAnimationCooldown > 0)animRect = swatting;

        var texture = AssetManager.GetTexture("FlySwatter-PlayerSpriteSheet.png");
        Raylib.DrawTexturePro(texture, animRect, new Rectangle(pos.X, pos.Y, size, size), new Vector2(size/2, size/2), angle, Raylib.WHITE);
    }

    public void Step(EntityManager m)
    {
        step++;
        if(swatAnimationCooldown > 0)swatAnimationCooldown--;
        float movement = 0;
        if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement = 1;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement -= 1;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            angle -= rotationSpeed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            angle += rotationSpeed;
        }
        pos += movement * new Vector2(MathF.Sin(angle * MathF.PI/180), -MathF.Cos(angle * MathF.PI/180)) * speed;

        //hit any units below the player
        foreach(var entity in m.Entities)
        {
            if(entity is SwatUnit swat && (pos - swat.pos).Length() <= size)
            {
                swatAnimationCooldown = 3;
                swat.TakeDamage(100);
            }
        }
    }
}