using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextureDownloadManager : MonoBehaviour
{
    #region Singleton Instance
    private static TextureDownloadManager _instance;
    public static TextureDownloadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObject = new GameObject("TextureDownloadManager");
                _instance = managerObject.AddComponent<TextureDownloadManager>();
                DontDestroyOnLoad(managerObject);
            }
            return _instance;
        }
    }
    #endregion

    Queue<TextureRequest> _downloadQueue = new Queue<TextureRequest>();
    int _maxConcurrentDownloads = 1;
    int _maxDownloadRetries = 10;
    List<TextureRequest> _currentDownloads = new List<TextureRequest>();

    void Update()
    {
        if (_currentDownloads.Count < _maxConcurrentDownloads)
        {
            if (_downloadQueue.Count > 0)
            {
                StartDownload(_downloadQueue.Dequeue());
            }
        }
    }

    public static void QueueTextureDownload(string url, Action<Texture2D> onComplete)
    {
        TextureRequest newRequest = new TextureRequest(url, onComplete);
        Instance._downloadQueue.Enqueue(newRequest);
        // Optionally sort the queue based on priority
    }

    private void StartDownload(TextureRequest request)
    {
        StartCoroutine(DownloadTextureCoroutine(request.Url, texture =>
        {
            request.OnComplete?.Invoke(texture);
            _currentDownloads.Remove(request);
        }));
        _currentDownloads.Add(request);
    }

    IEnumerator DownloadTextureCoroutine(string url, Action<Texture2D> callback)
    {
        Texture2D texture = null;
        string error = string.Empty;

        for (int i = 0; i < _maxDownloadRetries && texture == null; i++)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest(); // This waits for the request to complete without blocking the main thread

                if (request.result == UnityWebRequest.Result.Success)
                {
                    texture = DownloadHandlerTexture.GetContent(request);
                }
                else
                {
                    error = request.error;
                    if (i == _maxDownloadRetries - 1)
                    {
                        error = request.error;
                        Debug.Log("Error downloading texture after " + _maxDownloadRetries + " attempts: " + error);
                    }

                    // Wait a frame before trying again
                    yield return null;
                }
            }
        }

        if (texture != null)
        {
            yield return null;
            callback(texture);
        }
        else
        {
            Debug.Log("Error downloading texture " + error);
        }
    }
}

public class TextureRequest
{
    public string Url;
    public Action<Texture2D> OnComplete;

    public TextureRequest(string url, Action<Texture2D> onComplete)
    {
        Url = url;
        OnComplete = onComplete;
    }
}

