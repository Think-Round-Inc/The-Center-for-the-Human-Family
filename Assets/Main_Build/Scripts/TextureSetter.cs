using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSetter : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] Texture2D[] textures;

    private void Start() => SetPathColors();

    public void SetPathColors()
    {
        for (int i = 0; i < objects.Length; i++)
            if (i < textures.Length)
                if (objects[i].TryGetComponent(out Renderer renderer) && textures[i] != null)
                    renderer.material.SetTexture("_MainTex", textures[i]);
    }
}
