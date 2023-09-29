using UnityEngine;

public sealed class DomeWall : MonoBehaviour
{
    [SerializeField] Sprite[] artSprites;
    [SerializeField] Sprite[] religionSprites;
    private Renderer wallRenderer;
    private int currentIndex;

    private void Awake()
    {
        wallRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        ResetIndex();
        SwitchWallSprite();
    }
    
    public void ResetIndex() => currentIndex = -1;

    public void SwitchWallSprite()
    {
        DomeWallState currentWallState = DomeWallChanger.CurrentDomeWallState;
        print($"{gameObject.name} wall state == {currentWallState}");
        switch (currentWallState)
        {
            case DomeWallState.Art:
                if (wallRenderer == null) return;
                currentIndex++;
                if (currentIndex >= artSprites.Length)
                    currentIndex = 0;
                if (artSprites.Length > 0 && artSprites[currentIndex] != null)
                    wallRenderer.material.mainTexture = artSprites[currentIndex].texture;
                break;
            case DomeWallState.Religion:
                if (wallRenderer == null) return;
                currentIndex++;
                if (currentIndex >= religionSprites.Length)
                    currentIndex = 0;
                if (religionSprites[currentIndex] != null)
                    wallRenderer.material.mainTexture = religionSprites[currentIndex].texture;
                break;
        }
    }
}
