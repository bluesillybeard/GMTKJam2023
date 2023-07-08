//Humans, enemy because that's more generic
using System.Numerics;
using Raylib_CsLo;

public sealed class MainGameScene : IScene
{
    int step;
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
        step = 0;
    }
    public IScene Update()
    {
        step++;
        camera.zoom = 1;
        camera.offset = new Vector2(Raylib.GetRenderWidth()/2, Raylib.GetRenderHeight()/2);
        entities.Step();
        camera.target = entities.FindEntity<Player>()?.pos ?? Vector2.Zero;
        //every 10 seconds
        if(step % 60*10 == 0)
        {
            float randomAngle = Random.Shared.NextSingle() * 2*MathF.PI;
            Vector2 randomPos = new Vector2(MathF.Sin(randomAngle), MathF.Cos(randomAngle)) * 2000;
            for(int i=0; i<3; i++)
            {
                entities.AddEntity(new Exterminator(randomPos + new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle()) * 100));
            }
            
        }
        if((entities.FindEntity<SewageFactory>()?.health ?? 0) <= 0)
        {
            nextScene = new GameOverScreen(entities, camera);
        }
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
        var factory = entities.FindEntity<SewageFactory>();
        var hpRatio = (factory?.health ?? 0) / SewageFactory.startHealth;
        var hpWidth = Raylib.GetRenderWidth() - 60; //30 pixel margins
        Raylib.DrawRectangle(30, 30, hpWidth, 30, Raylib.BLACK);
        Raylib.DrawRectangle(30, 30, (int)(hpWidth*hpRatio), 30, Raylib.RED);
    }

    public void Dispose()
    {

    }
}