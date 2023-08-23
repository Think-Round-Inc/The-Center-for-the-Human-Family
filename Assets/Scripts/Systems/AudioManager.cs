using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

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
