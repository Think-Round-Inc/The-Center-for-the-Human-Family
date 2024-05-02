using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODShaderHandler : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private Texture2D _currentTexture;

    void Awake()
    {
        GetReferences();
    }

    void GetReferences()
    {
        if(_renderer == null) _renderer = GetComponent<Renderer>();
        if(_propBlock == null) _propBlock = new MaterialPropertyBlock();
    }

    public void ChangeTexture(Texture2D newTexture, float duration = 0f)
    {
        GetReferences();

        // Instantly Swap textures if changing in editor
        if(!Application.isPlaying)
        {
            _currentTexture = newTexture;
        }

        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetTexture("_TexA", _currentTexture ?? newTexture);
        _propBlock.SetTexture("_TexB", newTexture);
        _renderer.SetPropertyBlock(_propBlock);
        _currentTexture = newTexture;

        // Don't run coroutine if in editor
        if (Application.isPlaying)
        {
            StartCoroutine(TransitionLerp(duration));
        }
    }

    private IEnumerator TransitionLerp(float duration)
    {
        float time = 0.0f;
        while (time < duration)
        {
            float lerpValue = time / duration;
            SetLerp(lerpValue);
            time += Time.deltaTime;
            yield return null;
        }
        SetLerp(1.0f); // Ensure lerp completes

        // Reset textures to newTexture after transition completes
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetTexture("_TexA", _currentTexture);
        _propBlock.SetTexture("_TexB", _currentTexture);
        _propBlock.SetFloat("_Lerp", 0);
        _renderer.SetPropertyBlock(_propBlock);
    }

    private void SetLerp(float value)
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_Lerp", value);
        _renderer.SetPropertyBlock(_propBlock);
    }
}
