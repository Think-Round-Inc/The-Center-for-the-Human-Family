using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class UIButton : CustomUIComponent
{
    [SerializeField] ThemeSO theme;
    [SerializeField] StyleViewContainer style;
    [SerializeField] UnityEvent onClick;
    private Button button;
    private TextMeshProUGUI text;
    public override void Setup()
    {
        button = GetComponentInChildren<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        if (button || text == null) return;
        ColorBlock cb = button.colors;
        cb.normalColor = theme.GetBackgroundColor(style);
        button.colors = cb;
        text.color = theme.GetTextColor(style);
    }

    public void OnClick() => onClick?.Invoke();
}
