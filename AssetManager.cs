using System.Reflection.Metadata.Ecma335;
using Raylib_CsLo;


//TODO: cache loaded stuff
public static class AssetManager
{
    private static readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
    private static readonly Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
    private static readonly Dictionary<string, Music> musics = new Dictionary<string, Music>();


    public static Texture GetTexture(string name)
    {
        if(!textures.TryGetValue(name, out var texture))
        {
            texture = Raylib.LoadTexture(Environment.CurrentDirectory + "/assets/textures/" + name);
            textures.Add(name, texture);
        }
        return texture;
    }
    public static Sound GetSound(string name)
    {
        if(!sounds.TryGetValue(name, out var sound))
        {
            sound = Raylib.LoadSound(Environment.CurrentDirectory + "/assets/sounds/" + name);
            sounds.Add(name, sound);
        }
        return sound;
    }

    public static Music GetMusic(string name)
    {
        if(!musics.TryGetValue(name, out var music))
        {
            music = Raylib.LoadMusicStream(Environment.CurrentDirectory + "/assets/music/" + name);
            musics.Add(name, music);
        }
        return music;
    }

    public static void Update()
    {
        foreach(var music in musics.Values)
        {
            Raylib.UpdateMusicStream(music);
        }
    }

    public static void Dispose()
    {
        foreach(var texture in textures.Values)
        {
            Raylib.UnloadTexture(texture);
        }
        foreach(var sound in sounds.Values)
        {
            Raylib.UnloadSound(sound);
        }
        foreach(var music in musics.Values)
        {
            Raylib.UnloadMusicStream(music);
        }
        textures.Clear();
        sounds.Clear();
        musics.Clear();
    }
}