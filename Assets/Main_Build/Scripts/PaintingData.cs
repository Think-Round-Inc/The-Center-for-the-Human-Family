using UnityEngine;

[System.Serializable]
public class PaintingDataHolder
{
    public Sprite paintingImage;
    public string paintingName;
    public AudioClip paintingClip;
    [HideInInspector] public Color paintingTextureColor = Color.white;
}

[System.Serializable]
public sealed class PaintingData : MonoBehaviour
{
    public PaintingDataHolder paintingData;

    public void InitializePaintingData()
    {
        if (TryGetComponent<Renderer>(out var screenRenderer))
        {
            if (paintingData.paintingImage != null)
            {
                screenRenderer.material.color = paintingData.paintingTextureColor;
                screenRenderer.material.mainTexture = paintingData.paintingImage.texture;
            }
            else
                screenRenderer.material.color = SectionColorHolder.EmptyScreenColor;
        }
    }
}
