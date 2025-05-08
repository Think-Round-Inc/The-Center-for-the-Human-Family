using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class MainUIToolkitManager : MonoBehaviour
{
    private UIDocument m_MainUIDoc;
    private UIButtonWithHelperPanel m_LocationButton;

    private void Start()
    {
        StartCoroutine(FindButtonAndInitialize());
    }

    private IEnumerator FindButtonAndInitialize()
    {
        yield return new WaitForEndOfFrame();
        m_MainUIDoc = GetComponent<UIDocument>();
        if (m_MainUIDoc)
        {
            Debug.Log($"Found main ui document!");
            m_LocationButton = m_MainUIDoc.rootVisualElement.Q<UIButtonWithHelperPanel>("your-location-button");
        }

        if (m_LocationButton != null)
            m_LocationButton.clicked += () => { Debug.Log($"Location Button Clicked!"); };
    }
}
