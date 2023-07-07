//Humans, enemy because that's more generic
using System.Drawing;
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
        entities.AddEntity(new Player());
        entities.AddEntity(new SwatUnit());
    }
    public IScene Update()
    {
        camera.zoom = 1;
        camera.offset = new Vector2(Raylib.GetRenderWidth()/2, Raylib.GetRenderHeight()/2);
        entities.Step();
        foreach(var e in entities.FindEntities<Player>())
        {
            camera.target = e.pos;
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
        entities.Draw();
        Raylib.EndMode2D();
    }

    public void Dispose()
    {

    }
}