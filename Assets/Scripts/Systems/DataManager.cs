using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class DataManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] CanvasRenderer dataInfoPanel;
    [SerializeField] Image paintingZoomableCanvas;
    [SerializeField] UnityEvent onInfoPanelOpened;
    [SerializeField] UnityEvent onInfoPanelClosed;

    void Start()
    {
        dataInfoPanel.gameObject.SetActive(false);
    }

    public void PrintPaintingName(GameObject screen)
    {
        if (audioSource == null) return;
        if (screen.TryGetComponent<PaintingData>(out var data))
        {
            string paintingName = data.paintingData.paintingName;
            if (paintingName != string.Empty)
                print($"Currently near: {paintingName}");
            else
                print($"No Data Available");
        }
    }

    public void HideInfoDataPanel()
    {
        dataInfoPanel.gameObject.SetActive(false);
        onInfoPanelClosed?.Invoke();
    }

    public void ShowInfoDataPanel()
    {
        dataInfoPanel.gameObject.SetActive(true);
        onInfoPanelOpened?.Invoke();
    }

    public void HidePaintingImage() => paintingZoomableCanvas.gameObject.SetActive(false);
    
    public void DisplayPaintingNameOnCanvas(GameObject screen)
    {
        PaintingData data = screen.GetComponent<PaintingData>();
        for (int i = 0; i < dataInfoPanel.gameObject.transform.childCount; i++)
        {
            GameObject currentGO = dataInfoPanel.gameObject.transform.GetChild(i).gameObject;
            if (currentGO.name == "PaintingName") // hate using string compare as the name may accidentally get renamed...
            {
                if (currentGO.TryGetComponent(out TMP_Text text) && data != null)
                    text.text = $"{data.paintingData.paintingName}";
            }
        }
        if (data != null)
            ShowInfoDataPanel();
    }

    public void PlayAudioAtPainting(GameObject stand)
    {
        if (audioSource == null) return;
        if (stand.TryGetComponent<PaintingData>(out var data))
        {
            AudioClip clip = data.paintingData.paintingClip;
            if (clip != null)
            {
                if (clip == audioSource.clip)
                    audioSource.UnPause();
                else
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            }
            else
                audioSource.Pause();
        }
    }

    public void SetPaintingImageFromData(GameObject screen)
    {
        if (screen.TryGetComponent(out PaintingData data))
            if (data.paintingData.paintingImage != null)
                paintingZoomableCanvas.sprite = data.paintingData.paintingImage;
    }

    public void StopPlayingAudio()
    {
        if (audioSource == null) return;
        audioSource.Stop();
    }

    public void PauseAudio()
    {
        if (audioSource == null) return;
        audioSource.Pause();
    }
}
