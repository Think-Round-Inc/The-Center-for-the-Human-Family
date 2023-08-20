using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Style
{
    Primary,
    Secondary,
    Tertiary
}

public sealed class View : CustomUIComponent
{
    [SerializeField] ViewSO viewData;
    [SerializeField] GameObject containerTop;
    [SerializeField] GameObject containerCenter;
    [SerializeField] GameObject containerBottom;

    private Image topImage;
    private Image centerImage;
    private Image bottomImage;

    private VerticalLayoutGroup verticalLayout;

    public override void Configure()
    {
        verticalLayout.padding = viewData.padding;
        verticalLayout.spacing = viewData.spacing;
    }

    public override void Setup()
    {
        verticalLayout = GetComponent<VerticalLayoutGroup>();
        topImage = containerTop.GetComponent<Image>();
        centerImage = containerCenter.GetComponent<Image>();
        bottomImage = containerBottom.GetComponent<Image>();
        topImage.color = viewData.theme.top_bg;
        centerImage.color = viewData.theme.center_bg;
        bottomImage.color = viewData.theme.bottom_bg;
    }
}
