using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public AudioSource audioSource;

    public void Toggle_Volume(bool toggle)
    {
        Debug.Log(toggle);
        if(toggle == true)
        {
            Debug.Log("Music is on");
            audioSource.volume =  0.1f;
        }
        else if(toggle == false){
            Debug.Log("Music is off");
            audioSource.volume =  0f;
        }   
    }
}
