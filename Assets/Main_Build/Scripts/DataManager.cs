using UnityEngine;

public sealed class DataManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PrintPaintingName(GameObject screen)
    {
        PaintingData data = screen.GetComponentInChildren<PaintingData>();
        if (data != null)
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
        PaintingData data = stand.GetComponentInChildren<PaintingData>();
        if (data != null)
        {
            AudioClip clip = data.paintingData.paintingClip;
            if (clip != null)
            {
                if (clip == audioSource.clip && audioSource.isPlaying) return;
                audioSource.Stop();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
