using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomUIComponent : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    [Button("Configure")]
    private void Init()
    {
        Setup();
        Configure();
    }

    public abstract void Configure();

    public abstract void Setup();

    private void OnValidate()
    {
        Init();
    }
}
