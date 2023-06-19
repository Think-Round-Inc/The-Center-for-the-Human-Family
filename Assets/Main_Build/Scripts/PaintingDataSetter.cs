using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public sealed class PaintingDataSetter : MonoBehaviour
{
    [SerializeField] PaintingDataHolder[] paintingData;
    [SerializeField] GameObject[] screens;
    [SerializeField] Color paintingTextureColor;
    [SerializeField] Texture2D missingPaintingTexture;
    [SerializeField] Color missingPaintingTextureColor;

    private void Start()
    {
        SetPaintingData();
    }

    [Button("Set Painting Data")]
    public void SetPaintingData()
    {
        for (int i = 0; i < paintingData.Length; i++)
        {
            if (paintingData[i].paintingImage != null && screens[i] != null)
            {
                if (screens[i].TryGetComponent(out PaintingData data))
                {
                    data.paintingData.paintingImage = paintingData[i].paintingImage;
                    if (paintingData[i].paintingName != "")
                    {
                        data.paintingData.paintingTextureColor = paintingTextureColor;
                        data.paintingData.paintingName = paintingData[i].paintingName;
                        data.paintingData.paintingClip = paintingData[i].paintingClip;
                    }
                    else
                    {
                        if (missingPaintingTexture != null)
                            data.paintingData.paintingImage = missingPaintingTexture;
                        data.paintingData.paintingName = "";
                        data.paintingData.paintingTextureColor = paintingTextureColor;
                    }
                    EditorUtility.SetDirty(screens[i]);
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

