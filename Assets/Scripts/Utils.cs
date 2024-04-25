using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    #region Extension Methods

    #region MeshRenderer Extensions
    public static void ApplySpriteTextureToPropertyBlock(this MeshRenderer meshRend, Sprite sprite, int matIndex = 0)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRend.GetPropertyBlock(propBlock, matIndex);
        Texture2D texture = sprite == null ? null : sprite.texture;
        propBlock.SetTexture("_BaseMap", texture);
        meshRend.SetPropertyBlock(propBlock, matIndex);
    }
    #endregion

    #endregion
}
