using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DomeSectionGFX : MonoBehaviour
{
    [Header("Textures")]
    public Sprite TopSprite;

    [Header("References")]
    [SerializeField] MeshRenderer _topWallMesh;

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
        _topWallMesh.ApplySpriteTextureToPropertyBlock(TopSprite);
        _topWallMesh.ApplySpriteTextureToPropertyBlock(null, 1);
    }
}
