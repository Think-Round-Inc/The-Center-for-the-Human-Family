using UnityEngine;

public sealed class DomeWallTextureSetter : MonoBehaviour
{
    [SerializeField] GameObject[] domeWallObjects;
    [SerializeField] Texture2D[] domeWallTextures;
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
