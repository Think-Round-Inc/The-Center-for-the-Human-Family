using UnityEngine;

public sealed class DataManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PrintPaintingName(GameObject screen)
    {
        if (audioSource == null) return;
        if (screen.TryGetComponent<PaintingData>(out var data))
        {
            string paintingName = data.paintingData.paintingName;
            if (paintingName != "")
                print($"Currently near: {paintingName}");
            else
                print($"No Data Available");
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
