using UnityEngine;

public sealed class PaintingLoaderSystemHandler : MonoBehaviour
{
    public static bool LoadImagesFromWeb = true;
    [SerializeField] bool loadImagesFromWeb = true;

    private void OnValidate()
    {
        LoadImagesFromWeb = loadImagesFromWeb;
    }
}
