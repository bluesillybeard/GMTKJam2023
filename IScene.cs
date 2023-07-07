using Raylib_CsLo;

public interface IScene : IDisposable
{
    IScene Update();
    void Frame();
}