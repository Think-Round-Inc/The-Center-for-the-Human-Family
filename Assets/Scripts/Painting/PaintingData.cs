using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PaintingData
{
    public Sprite Image;
    public string Name;
    [TextArea(0, 10)] public string Info;
    public AudioClip Audio;
    public string URL = "https://picsum.photos/seed/picsum/200/300";
}
