//Humans, enemy because that's more generic
using System.Numerics;
using Raylib_CsLo;

public sealed class MainGameScene : IScene
{
    IScene nextScene;
    Camera2D camera;
    EntityManager entities;

    public MainGameScene()
    {
        entities = new EntityManager();
        camera = new Camera2D();
        nextScene = this;
        entities.AddEntity(new SewageFactory());
        entities.AddEntity(new Player());

    }
    public IScene Update()
    {
        camera.zoom = 1;
        camera.offset = new Vector2(Raylib.GetRenderWidth()/2, Raylib.GetRenderHeight()/2);
        entities.Step();
        camera.target = entities.FindEntity<Player>()?.pos ?? Vector2.Zero;

        float randomAngle = Random.Shared.NextSingle() * 2*MathF.PI;
        Vector2 randomPos = new Vector2(MathF.Sin(randomAngle), MathF.Cos(randomAngle)) * 1000;
        entities.AddEntity(new SwatUnit(randomPos));
        if(nextScene != this)
        {
            Dispose();
        }
        return nextScene;
    }
    public void Frame()
    {
        Raylib.BeginMode2D(camera);
        var texture = AssetManager.GetTexture("background.png");
        Raylib.DrawTexturePro(texture, new Rectangle(0, 0, texture.width, texture.height), new Rectangle(-20000, -20000, 40000, 40000), Vector2.Zero, 0, Raylib.WHITE);
        entities.Draw();
        Raylib.EndMode2D();
    }

    public void Dispose()
    {

    }
}