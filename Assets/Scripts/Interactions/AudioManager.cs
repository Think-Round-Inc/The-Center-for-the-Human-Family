using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
