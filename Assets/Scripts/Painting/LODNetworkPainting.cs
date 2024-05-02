using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LODShaderHandler))]
public class LODNetworkPainting : MonoBehaviour
{
    [Header("Data")]
    public PaintingData Data;

    [field: Header("State")]
    [field: SerializeField] public bool IsCameraNear;
    [field: SerializeField] public Texture2D LoadedTexture { get; private set; }

    Camera _mainCam;
    LODShaderHandler _lodShaderHandler;
    float _transitionTime = 0.25f;

    private void OnValidate()
    {
        GetReferences();
        LoadedTexture = Data.Image.texture;
        UpdateMat(0f);
    }

    private void Awake()
    {
        GetReferences();
        _mainCam = Camera.main;
        LoadedTexture = Data.Image.texture;
    }

    void GetReferences()
    {
        _lodShaderHandler = GetComponent<LODShaderHandler>();
    }

    void UpdateMat(float duration = 0f)
    {
        _lodShaderHandler.ChangeTexture(LoadedTexture, duration);
    }

    void OnCameraGetNear()
    {
        TextureDownloadManager.QueueTextureDownload(Data.URL, (Texture2D downloadedTexture) =>
        {
            if (!IsCameraNear) return; // Abort if the player moved away durring load
            LoadedTexture = downloadedTexture;
            UpdateMat(_transitionTime);
        });
    }

    void OnCameraGetFar()
    {
        LoadedTexture = Data.Image.texture;
        UpdateMat(_transitionTime);
    }

    #region Trigger
    void HandleTrigger(Collider other, bool isEntering)
    {
        if (other.tag != "MainCamera") return;

        // Camera is getting closer
        if (!IsCameraNear && isEntering)
        {
            OnCameraGetNear();
        }

        // Camera is getting farther
        if (IsCameraNear && !isEntering)
        {
            OnCameraGetFar();
        }

        IsCameraNear = isEntering;
    }
    private void OnTriggerEnter(Collider other) => HandleTrigger(other, true);

    private void OnTriggerStay(Collider other) { if(!IsCameraNear) HandleTrigger(other, true); }

    private void OnTriggerExit(Collider other) => HandleTrigger(other, false);
    #endregion
}