using NaughtyAttributes;
using UnityEngine;

public sealed class DomeWallTextureSetter : MonoBehaviour
{
    [SerializeField] GameObject[] domeWallObjects;
    [SerializeField, InfoBox("0: ceiling, 1: Christains, 2: Jews, 3: Buddhists, 4: Hindus, 5: Taoists, 6: Indigenous, 7: Muslim.")] Texture2D[] domeWallTextures;
    [SerializeField] Color domeWallTextureColor = Color.white;

    private void Start()
    {
        InitializeDomeTextures();
    }

    public void InitializeDomeTextures()
    {
        for (int i = 0; i < domeWallObjects.Length; i++)
        {
            if (i < domeWallTextures.Length)
            {
                if (domeWallObjects[i] == null || domeWallTextures[i] == null) return;
                if (domeWallObjects[i].TryGetComponent(out Renderer renderer))
                {
                    renderer.material.color = domeWallTextureColor;
                    renderer.material.mainTexture = domeWallTextures[i];
                }
            }
        }
    }
}
