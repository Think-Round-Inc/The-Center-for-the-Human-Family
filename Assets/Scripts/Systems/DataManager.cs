using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class DataManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] CanvasRenderer dataInfoPanel;
    [SerializeField] Image paintingZoomableCanvas;
    [SerializeField] TMP_Text infoText;
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

    public void ClearInfoPanelText() => infoText.text = string.Empty;

    public void SetPaintingNameAndInfoToInfoText(GameObject screen)
    {
        if (screen.TryGetComponent(out PaintingData data))
            infoText.text = $"{data.paintingData.paintingName}\n{data.paintingData.paintingInfo}";
    }
    
    public void DisplayPainting(GameObject screen)
    {
        if (screen.TryGetComponent(out PaintingData data))
        {
            if (data.paintingData.paintingImage != null)
                paintingZoomableCanvas.sprite = data.paintingData.paintingImage;
        }
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
