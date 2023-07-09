using System.Numerics;
using Raylib_CsLo;

public sealed class MainMenuScreen : IScene
{
    const int difficultyButtonSize = 300;
    const int playButtonSize = 300;
    const int logoSize = 800;
    static readonly Rectangle difficultyButton = new Rectangle(1240/2 - difficultyButtonSize/2, 280, difficultyButtonSize, difficultyButtonSize*59/176);
    static readonly Rectangle easySrc = new Rectangle(220, 130, 176, 59);
    static readonly Rectangle mediumSrc = new Rectangle(219, 191, 176, 59);
    static readonly Rectangle hardSrc = new Rectangle(221, 66, 176, 59);
    static readonly Rectangle insaneSrc = new Rectangle(139, 253, 176, 59);
    int difficulty = 3;
    static readonly Rectangle playButton = new Rectangle(1240/2 - playButtonSize/2, 400, playButtonSize, playButtonSize*77/157);
    static readonly Rectangle playButtonSrc = new Rectangle(340, 265, 157, 77);
    static readonly Rectangle logo = new Rectangle(1240/2 - logoSize/2, 60, logoSize, logoSize*39/149);
    static readonly Rectangle logoSrc = new Rectangle(41, 328, 149, 39);
    static readonly Vector2 howToPlayText = new Vector2(0, 30);
    public IScene Update()
    {
        Vector2 cursorPos = Raylib.GetMousePosition();
        if(Raylib.CheckCollisionPointRec(cursorPos, playButton) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            System.Console.WriteLine("Difficulty " + difficulty);
            return new MainGameScene(difficulty);
        }

        if(Raylib.CheckCollisionPointRec(cursorPos, difficultyButton) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            difficulty = (difficulty+3)%4;
        }
        return this;
    }

    public void Frame()
    {
        Vector2 cursorPos = Raylib.GetMousePosition();
        Raylib.ClearBackground(new Color(108, 30, 209, 255));
        var buttonsTexture = AssetManager.GetTexture("buttons.png");
        Rectangle difficultyButtonSrc;
        switch(difficulty)
        {
            case 0: difficultyButtonSrc = insaneSrc;break;
            case 1: difficultyButtonSrc = hardSrc;break;
            case 2: difficultyButtonSrc = mediumSrc;break;
            case 3: difficultyButtonSrc = easySrc;break;
            default: throw new Exception("Invalid difficulty");
        }
        Raylib.DrawTexturePro(buttonsTexture, logoSrc, logo, Vector2.Zero, 0, Raylib.WHITE);
        var difficultyButtonRec = difficultyButton;
        if(Raylib.CheckCollisionPointRec(cursorPos, difficultyButton))
        {
            difficultyButtonRec.X -= difficultyButtonRec.width/10;
            difficultyButtonRec.Y -= difficultyButtonRec.height/10;
            difficultyButtonRec.width += difficultyButtonRec.width*2/10;
            difficultyButtonRec.height += difficultyButtonRec.height*2/10;
        }

        Raylib.DrawTexturePro(buttonsTexture, difficultyButtonSrc, difficultyButtonRec, Vector2.Zero, 0, Raylib.WHITE);

        var playButtonRec = playButton;
        if(Raylib.CheckCollisionPointRec(cursorPos, playButton))
        {
            playButtonRec.X -= playButtonRec.width/10;
            playButtonRec.Y -= playButtonRec.height/10;
            playButtonRec.width += playButtonRec.width*2/10;
            playButtonRec.height += playButtonRec.height*2/10;
        }
        Raylib.DrawTexturePro(buttonsTexture, playButtonSrc, playButtonRec, Vector2.Zero, 0, Raylib.WHITE);
    }

    public void Dispose()
    {

    }
}