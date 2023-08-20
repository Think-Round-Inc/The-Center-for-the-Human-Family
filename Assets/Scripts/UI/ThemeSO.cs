using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ThemeSO", fileName = "ThemeSO")]
public class ThemeSO : ScriptableObject
{
    [Header("Top")] 
    public Color top_bg;
    public Color top_text;
    [Header("Center")]
    public Color center_bg;
    public Color center_text;
    [Header("Bottom")]
    public Color bottom_bg;
    public Color bottom_text;
    [Header("Other")]
    public Color disable;

    public Color GetBackgroundColor(Style style)
    {
        return style switch
        {
            Style.Primary => top_bg,
            Style.Secondary => center_bg,
            Style.Tertiary => bottom_bg,
            _ => disable,
        };
    }

    public Color GetTextColor(Style style)
    {
        return style switch
        {
            Style.Primary => top_text,
            Style.Secondary => center_text,
            Style.Tertiary => bottom_text,
            _ => disable,
        };
    }
}
