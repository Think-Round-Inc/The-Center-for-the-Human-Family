using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/TextSO", fileName = "TextSO")]
public class TextSO : ScriptableObject
{
    public ThemeSO theme;
    public TMP_FontAsset font;
    public float size;
}
