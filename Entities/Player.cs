using System.Numerics;
using Raylib_CsLo;

public sealed class Player : IEntity
{
    const float speed = 10;
    const float rotationSpeed = 4;
    public Vector2 pos = Vector2.Zero;
    float angle = 0;
    public void Draw()
    {
        //TODO: actual player sprite
        Raylib.DrawPoly(pos, 3, 20, angle, Raylib.WHITE);
    }

    public void Step(EntityManager m)
    {
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
            angle += rotationSpeed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            angle -= rotationSpeed;
        }
        pos += movement * new Vector2(MathF.Sin(angle * MathF.PI/180), MathF.Cos(angle * MathF.PI/180)) * speed;
    }
}