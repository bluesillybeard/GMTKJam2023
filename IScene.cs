public interface IScene : IDisposable
{
    void PreFrame();
    IScene Frame();
}