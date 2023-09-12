using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthLayerController : MonoBehaviour
{
    [SerializeField] Toggle[] toggles = new Toggle[4];
    [SerializeField] Image[] images;
    [SerializeField] CanvasRenderer layerPanelParent;
    [SerializeField] string layerPanelName;

    public void UpdateSprites()
    {
        int toggleState = 0;

        if (toggles[0].isOn) toggleState += 1;
        if (toggles[1].isOn) toggleState += 2;
        if (toggles[2].isOn) toggleState += 4;
        if (toggles[3].isOn) toggleState += 8;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == toggleState);
        }
    }

    public void CheckIfPanelIsLayers(EarthViewPanel panel)
    {
        if (panel.panelName == layerPanelName)
        {
            layerPanelParent.gameObject.SetActive(true);
        }
        else
            layerPanelParent.gameObject.SetActive(false);
    }

    public void SingleLayerSelect(int togglePressed)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i == togglePressed)
                toggles[i].isOn = true;
            else
                toggles[i].isOn = false;
        }
        UpdateSprites();
    }

    // not working or need to rework
    public void SingleLayerDeselect(int togglePressed)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i == togglePressed)
                toggles[i].isOn = false;
            else
                toggles[i].isOn = false;
        }
        UpdateSprites();
    }
}
