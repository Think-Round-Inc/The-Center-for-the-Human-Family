using UnityEngine;

[ExecuteInEditMode]
public class DomeSectionGFX : MonoBehaviour
{
    [Header("Top")]
    public Sprite TopSprite;
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
        _topMesh.ApplySpriteTextureToPropertyBlock(null, 1);

        // Top Mesh UV Offset
        Vector2[] uvs = _topMeshFilter.sharedMesh.uv;
        for(int i = 0; i < uvs.Length; i++) uvs[i] += TopOffset;
        _topMeshFilter.mesh.uv = uvs;
    }
}
