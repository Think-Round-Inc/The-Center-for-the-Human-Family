using UnityEngine;

public sealed class DomeWall : MonoBehaviour
{
    [SerializeField] Sprite[] wallSprites;
    private Renderer wallRenderer;
    private int currentIndex;

    private void Awake()
    {
        wallRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        currentIndex = -1;
        SwitchWallSprite();
    }

    public void SwitchWallSprite()
    {
        if (wallRenderer == null) return;
        currentIndex++;
        if (currentIndex >= wallSprites.Length)
            currentIndex = 0;
        if (wallSprites[currentIndex] != null)
            wallRenderer.material.mainTexture = wallSprites[currentIndex].texture;
    }
}
