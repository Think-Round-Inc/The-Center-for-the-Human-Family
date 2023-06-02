
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemAssets", order = 1)]
public class ItemAssets : ScriptableObject
{
    public string[] texts;
    public AudioClip[] audioClips;
}
