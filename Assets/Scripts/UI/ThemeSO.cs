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

    public Color GetBackgroundColor(StyleViewContainer style)
    {
        return style switch
        {
            StyleViewContainer.Top => top_bg,
            StyleViewContainer.Center => center_bg,
            StyleViewContainer.Bottom => bottom_bg,
            _ => disable,
        };
    }

    public Color GetTextColor(StyleViewContainer style)
    {
        return style switch
        {
            StyleViewContainer.Top => top_text,
            StyleViewContainer.Center => center_text,
            StyleViewContainer.Bottom => bottom_text,
            _ => disable,
        };
    }
}
