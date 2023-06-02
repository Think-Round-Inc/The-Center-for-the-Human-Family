using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlayButtonClicked : MonoBehaviour
{
   public ItemAssets itemAssets;
   public GameObject UIField;
    public Text textField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClicked(int itemNumber){
        UIField.SetActive(true);
        setText(itemNumber);
        setAudio(itemNumber);

    }

    private void setText(int itemNumber){
        textField.text = itemAssets.texts[itemNumber];
    }

    private void setAudio(int itemNumber){
        AudioManager.instance.audioSource.clip = itemAssets.audioClips[itemNumber];
    }
}
