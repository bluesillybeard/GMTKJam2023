//Humans, enemy because that's more generic
using System.Globalization;
using System.Numerics;
using System.Xml.Schema;
using Raylib_CsLo;

public sealed class MainGameScene : IScene
{
    int step;
    int wave;
    public static readonly float[][] waveDifficulties = 
    new float[][]{
        new float[12]{10, 5, 3, 1, 0.5f, 0.25f, 0.15f, 0.1f, 0.07f, 0.03f, 0.01f, 0f},
        new float[12]{12, 10, 8, 7, 5, 4, 3, 2, 1.8f, 1.3f, 1, 0.5f},
        new float[12]{17, 15, 10, 8, 8, 7, 5, 5, 3, 3, 1, 1},
        new float[12]{20, 18, 15, 12, 10, 9, 9, 8, 8, 7, 6, 5},
    };

    int difficulty;
    IScene nextScene;
    Camera2D camera;
    EntityManager entities;

    public MainGameScene(int difficulty)
    {
        this.difficulty = difficulty;
        entities = new EntityManager();
        camera = new Camera2D();
        nextScene = this;
        entities.AddEntity(new SewageFactory());
        entities.AddEntity(new Player());
        step = 0;
        float randomAngle = Random.Shared.NextSingle() * 2*MathF.PI;
        Vector2 randomPos = new Vector2(MathF.Sin(randomAngle), MathF.Cos(randomAngle)) * 5500;
        entities.AddEntity(new Exterminator(randomPos + new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle()) * 150));
    }
    public IScene Update()
    {
        step++;
        camera.zoom = 0.5f;
        camera.offset = new Vector2(Raylib.GetRenderWidth()/2, Raylib.GetRenderHeight()/2);
        entities.Step();
        camera.target = entities.FindEntity<Player>()?.pos ?? Vector2.Zero;
        if(wave < 12)
        {
            if((int)(step % (60*waveDifficulties[difficulty][wave])) == 0)
            {
                float randomAngle = Random.Shared.NextSingle() * 2*MathF.PI;
                Vector2 randomPos = new Vector2(MathF.Sin(randomAngle), MathF.Cos(randomAngle)) * 5500;
                for(int i=0; i<2; i++)
                {
                    entities.AddEntity(new SwatUnit(randomPos + new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle()) * 150));
                }
                
            }
            //TODO: spawn frequency of exterminators
            if(step % (60*10) == 0)
            {
                float randomAngle = Random.Shared.NextSingle() * 2*MathF.PI;
                Vector2 randomPos = new Vector2(MathF.Sin(randomAngle), MathF.Cos(randomAngle)) * 5500;
                for(int i=0; i<1; i++)
                {
                    entities.AddEntity(new Exterminator(randomPos + new Vector2(Random.Shared.NextSingle(), Random.Shared.NextSingle()) * 150));
                }
            }
        }
        //12 waves over the span of 10 minutes = 50 seconds per wave
        if(step % (60*50) == 0)
        {
            System.Console.WriteLine("wave" + wave);
            wave++;
        }
        if(wave >= 12)
        {
            //If there no more enemy units
            if(
            entities.FindEntity<SwatUnit>() == null
            // && entities.FindEntity<Exterminator>() == null
            )
            {
                nextScene = new WinGameScreen(entities, camera);
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

        //draw the stopwatch
        var stopwatchTexture = AssetManager.GetTexture("stopwatch.png");
        Rectangle stopwatchBase = new Rectangle(34, 21, 31,37);
        Rectangle stopwatchHand = new Rectangle(82, 33, 3, 13);
        Vector2 screenSize = new Vector2(Raylib.GetRenderWidth(), Raylib.GetRenderHeight());
        const int stopwatchSize = 80;
        Rectangle stopwatchD = new Rectangle(screenSize.X - stopwatchSize-30, 70, stopwatchSize, stopwatchSize*37/31);
        Raylib.DrawTexturePro(stopwatchTexture, stopwatchBase, stopwatchD, Vector2.Zero, 0, Raylib.WHITE);

        //12 waves of 50 seconds each
        float percent = step / (60f*50*12);
        float angle = percent * 360;
        Vector2 stopwatchCenter = new Vector2(stopwatchD.X + stopwatchD.width/2, stopwatchD.y + stopwatchD.height/2 + stopwatchSize*4/37);
        Raylib.DrawTexturePro(stopwatchTexture, stopwatchHand, 
        new Rectangle(stopwatchCenter.X, stopwatchCenter.Y, stopwatchSize*3/31, stopwatchSize*37/63), 
        new Vector2(stopwatchSize*1.5f/31, stopwatchD.height/2 - stopwatchSize*3/37), 
        angle, Raylib.WHITE);
    }

    public void Dispose()
    {

    }
}