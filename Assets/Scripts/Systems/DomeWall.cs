using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public sealed class DomeWallSpriteGroup
{
    public Sprite[] groupSprites;
    public string[] groupTitles;
    [TextArea(0, 20)] public string[] groupInfo;
}


public sealed class DomeWall : MonoBehaviour
{
    [SerializeField] DomeWallSpriteGroup artGroup;
    [SerializeField] DomeWallSpriteGroup religionGroup;
    [SerializeField, HorizontalLine] UnityEvent onDomeWallSwitched;
    private Sprite currentSprite;
    private string currentTitle;
    private string currentInfo;
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
    
    public string GetCurrentTitle() => currentTitle;

    public string GetCurrentInfo() => currentInfo;

    public Sprite GetCurrentSprite() => currentSprite;

    public void ResetIndex() => currentIndex = -1;

    public void SwitchWallSprite()
    {
        DomeWallState currentWallState = DomeWallChanger.CurrentDomeWallState;
        switch (currentWallState)
        {
            case DomeWallState.Art:
                SwitchFromGroup(artGroup);
                break;
            case DomeWallState.Religion:
                SwitchFromGroup(religionGroup);
                break;
        }
        onDomeWallSwitched?.Invoke();
    }

    void SwitchFromGroup(DomeWallSpriteGroup spriteGroup)
    {
        if (wallRenderer == null) return;
        currentIndex++;
        if (currentIndex >= spriteGroup.groupSprites.Length)
            currentIndex = 0;
        if (spriteGroup.groupSprites.Length > 0)
            currentSprite = spriteGroup.groupSprites[currentIndex];
        if (spriteGroup.groupTitles.Length > 0 && currentIndex < spriteGroup.groupTitles.Length)
            currentTitle = spriteGroup.groupTitles[currentIndex];
        if (spriteGroup.groupInfo.Length > 0 && currentIndex < spriteGroup.groupInfo.Length)
            currentInfo = spriteGroup.groupInfo[currentIndex];
        if (currentSprite != null)
            wallRenderer.material.mainTexture = currentSprite.texture;
    }
}
