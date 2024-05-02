using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Utils
{
    #region Extension Methods

    #region MeshRenderer Extensions
    public static void ApplySpriteTextureToPropertyBlock(this MeshRenderer meshRend, Sprite sprite, string textureName = "_BaseMap", int matIndex = 0)
    {
        Texture2D texture = sprite == null ? new Texture2D(1, 1) : sprite.texture;
        meshRend.ApplyTextureToPropertyBlock(texture, textureName, matIndex);
    }

    public static void ApplyTextureToPropertyBlock(this MeshRenderer meshRend, Texture2D texture, string textureName = "_BaseMap", int matIndex = 0)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRend.GetPropertyBlock(propBlock, matIndex);
        Texture2D newTexture = texture == null ? new Texture2D(1, 1) : texture;
        propBlock.SetTexture(textureName, newTexture);
        meshRend.SetPropertyBlock(propBlock, matIndex);
    }
    #endregion

    #endregion
}
