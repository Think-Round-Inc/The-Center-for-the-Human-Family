using TMPro;
using UnityEngine;

public sealed class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] navigationInfoLabels;
    [SerializeField] TMP_Text hotspotInfoText;
    [SerializeField] float paintingNameTextSize;
    [SerializeField] float paintingInfoTextSize;

    private void Start()
    {
        SetAllNavigationLabelsInactive();
    }


    public void SetPaintingNameAndInfoToInfoText(GameObject screen)
    {
        if (screen.TryGetComponent(out PaintingData data))
            hotspotInfoText.text = $"<size={paintingNameTextSize}>{data.paintingData.paintingName}</size>\n\n<size={paintingInfoTextSize}>{data.paintingData.paintingInfo}</size>";
    }

    public void ClearInfoPanelText() => hotspotInfoText.text = string.Empty;

    public void SetNavigationInfoLabelActive(int index)
    {
        for (int i = 0; i < navigationInfoLabels.Length; i++)
        {
            if (i == index && navigationInfoLabels[i] != null)
                navigationInfoLabels[i].SetActive(true);
            else
                navigationInfoLabels[i].SetActive(false);
        }
    }

    public void SetAllNavigationLabelsInactive()
    {
        for (int i = 0; i < navigationInfoLabels.Length; i++)
        {
            if (navigationInfoLabels[i] != null)
                navigationInfoLabels[i].SetActive(false);
        }
    }
}
