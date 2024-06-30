using UnityEditor;
using UnityEngine;

[System.Serializable]
public sealed class PaintingDataSetter : MonoBehaviour
{
    [SerializeField] PaintingDataHolder[] paintingData;
    [SerializeField] GameObject[] screens;
    [SerializeField] Color paintingTextureColor;
    [SerializeField] Sprite missingPaintingTexture;

    private void Start()
    {
        SetPaintingData();
    }

    public void SetPaintingData()
    {
        for (int i = 0; i < paintingData.Length; i++)
        {
            if (i >= screens.Length) return;
            if (paintingData[i].paintingImage != null && screens[i] != null)
            {
                if (screens[i].TryGetComponent(out StandPaintingSetter_Old data))
                {
                    data.paintingData.paintingImage = paintingData[i].paintingImage;
                    if (paintingData[i].paintingName != string.Empty)
                    {
                        data.paintingData.paintingTextureColor = paintingTextureColor;
                        data.paintingData.paintingName = paintingData[i].paintingName;
                        data.paintingData.paintingClip = paintingData[i].paintingClip;
                        data.paintingData.extraPaintingInfo = paintingData[i].extraPaintingInfo;
                    }
                    else
                    {
                        if (missingPaintingTexture != null)
                            data.paintingData.paintingImage = missingPaintingTexture;
                        data.paintingData.paintingName = string.Empty;
                        data.paintingData.paintingTextureColor = paintingTextureColor;
                    }
#if UNITY_EDITOR
                    EditorUtility.SetDirty(screens[i]);
#endif
                }
            }
        }
#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
}

