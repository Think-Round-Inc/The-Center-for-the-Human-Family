using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject[] navigationInfoLabels;

    private void Start()
    {
        SetAllNavigationLabelsInactive();
    }

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
