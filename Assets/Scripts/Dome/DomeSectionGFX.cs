using UnityEngine;

[ExecuteInEditMode]
public class DomeSectionGFX : MonoBehaviour
{
    [Header("Top")]
    public Sprite TopSprite;
    public float TopScale;
    public Vector2 TopOffset;

    [Header("References")]
    [SerializeField] MeshRenderer _topMesh;
    [SerializeField] MeshFilter _topMeshFilter;

    private void Awake()
    {
        UpdateGFX();
    }

    private void OnValidate()
    {
        UpdateGFX();
    }

    void UpdateGFX()
    {
        UpdateTopTexture();
    }

    void UpdateTopTexture()
    {
        // Apply Top Sprite
        _topMesh.ApplySpriteTextureToPropertyBlock(TopSprite);
        _topMesh.ApplySpriteTextureToPropertyBlock(null, matIndex: 1);
    }
}
