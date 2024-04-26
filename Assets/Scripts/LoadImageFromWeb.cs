using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageFromWeb : MonoBehaviour
{
    public string imageUrl = "http://www.example.com/image.jpg"; // URL of the image
    public RawImage displayImage; // Reference to UI RawImage component, use Renderer for 3D/2D objects

    void Start()
    {
        StartCoroutine(DownloadImage(imageUrl));
    }

    IEnumerator DownloadImage(string url)
    {
        Debug.Log("Downlaoding: " + url);
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        Debug.Log(request.result);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Success");
            // Apply the downloaded texture as a material to the object
            Texture2D webTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            displayImage.texture = webTexture; // Use this for UI Image
            // GetComponent<Renderer>().material.mainTexture = webTexture; // Use this line for 3D/2D Renderer
        }
    }
}
