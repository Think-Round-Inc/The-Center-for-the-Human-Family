using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DomeWallController : MonoBehaviour
{
    [SerializeField] Image currentDomeImage;
    [SerializeField] TMP_Text currentDomeTitleText;
    [SerializeField] TMP_Text currentDomeInfoText;

    public void UpdateDomeWallViewingPanel(GameObject hotSpotObject)
    {
        if (hotSpotObject.TryGetComponent(out DomeWall domeWall))
        {
            currentDomeImage.sprite = domeWall.GetCurrentSprite();
            
            string title = domeWall.GetCurrentTitle();
            
            if (title != string.Empty)
                currentDomeTitleText.text = title;
            else
                currentDomeTitleText.text = "Data coming soon!";
            
            string info = domeWall.GetCurrentInfo();
            if (info != string.Empty)
                currentDomeInfoText.text = info;
            else
                currentDomeInfoText.text = "Group:\n\nContinent:\n\nArtist:\n\nYear:\n\nMedium:\n\nLocation:";
        }
    }
}
