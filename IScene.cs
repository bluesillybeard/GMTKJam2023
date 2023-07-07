using Raylib_CsLo;

public interface IScene : IDisposable
{
    void PreFrame();
    IScene Frame();
}