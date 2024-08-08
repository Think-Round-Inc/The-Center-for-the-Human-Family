using UnityEngine;

[System.Serializable]
public class PaintingData
{
    public Sprite Image;
    public string Name;
    [TextArea(0, 10)] public string Info;
    public AudioClip Audio;
    // track down image and set some "default" image instead (no painting)
    public string URL = "https://picsum.photos/seed/picsum/200/300";
}
