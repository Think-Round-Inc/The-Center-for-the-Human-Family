using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Utils
{
    public static async Task<Texture2D> DownloadImage(string url, int maxTries = 10)
    {
        string error = string.Empty;
        for (int i = 0; i < maxTries; i++)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
   
            request.SendWebRequest();
            while (request.result == UnityWebRequest.Result.InProgress) await Task.Yield(); // wait 1 frame until request done
            if (request.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(request);
            }

            if(i == maxTries-1)
            {
                error = request.error;
            }
        }

        Debug.Log("Error: " + error);
        return null;
    }

    #region Extension Methods

    #region MeshRenderer Extensions
    public static void ApplySpriteTextureToPropertyBlock(this MeshRenderer meshRend, Sprite sprite, int matIndex = 0)
    {
        Texture2D texture = sprite == null ? new Texture2D(1, 1) : sprite.texture;
        meshRend.ApplyTextureToPropertyBlock(texture, matIndex);
    }

    public static void ApplyTextureToPropertyBlock(this MeshRenderer meshRend, Texture2D texture, int matIndex = 0)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRend.GetPropertyBlock(propBlock, matIndex);
        Texture2D newTexture = texture == null ? new Texture2D(1, 1) : texture;
        propBlock.SetTexture("_BaseMap", newTexture);
        meshRend.SetPropertyBlock(propBlock, matIndex);
    }
    #endregion

    #endregion
}
