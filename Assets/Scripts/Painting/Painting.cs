using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    [Header("Data")]
    public PaintingData Data;

    [field: Header("State")]
    [field: SerializeField] public bool IsCameraNear;
    [field: SerializeField] public Texture2D LoadedTexture { get; private set; }

    Camera _mainCam;

    private void OnValidate()
    {
        LoadedTexture = Data.Image.texture;
        UpdateMat();
    }

    private void Awake()
    {
        _mainCam = Camera.main;
        LoadedTexture = Data.Image.texture;
    }

    void UpdateMat()
    {
        GetComponent<MeshRenderer>().ApplyTextureToPropertyBlock(LoadedTexture);
    }
    
    void OnCameraGetNear()
    {
        Data.DownloadURLImage((Texture2D downloadedTexture) =>
        {
            if (!IsCameraNear) return; // Abort if the player moved away durring load
            LoadedTexture = downloadedTexture;
            UpdateMat();
        });
    }

    void OnCameraGetFar()
    {
        LoadedTexture = Data.Image.texture;
        UpdateMat();
    }

    #region Trigger
    void HandleTrigger(Collider other, bool isEntering)
    {
        if (other.tag != "MainCamera") return;

        // Camera is getting closer
        if(!IsCameraNear && isEntering)
        {
            OnCameraGetNear();
        }

        // Camera is getting farther
        if(IsCameraNear && !isEntering)
        {
            OnCameraGetFar();
        }

        IsCameraNear = isEntering;
    }
    private void OnTriggerEnter(Collider other) => HandleTrigger(other, true);

    private void OnTriggerStay(Collider other) => HandleTrigger(other, true);

    private void OnTriggerExit(Collider other) => HandleTrigger(other, false);
    #endregion
}