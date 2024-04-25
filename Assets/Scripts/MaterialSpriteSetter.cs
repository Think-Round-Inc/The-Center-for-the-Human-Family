using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class MaterialSpriteSetter : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    private MeshRenderer _meshRend;
    private MaterialPropertyBlock _propBlock;

    private void OnValidate()
    {
        UpdateMat();
    }

    private void Awake()
    {
        UpdateMat();
    }

    void GetReferences()
    {
        if (_meshRend == null) _meshRend = GetComponent<MeshRenderer>();
        if(_propBlock == null) _propBlock = new MaterialPropertyBlock();
    }

    void UpdateMat()
    {
        GetReferences();
        if (_sprite == null) return; // Check if the sprite is not assigned

        // Get the current property block, modify it, and set it back
        _meshRend.GetPropertyBlock(_propBlock);
        _propBlock.SetTexture("_MainTex", _sprite.texture);
        _meshRend.SetPropertyBlock(_propBlock);
    }
}
