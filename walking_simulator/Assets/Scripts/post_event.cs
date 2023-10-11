using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class post_event : MonoBehaviour
{   
    public AudioSource sound_01;
    public AudioSource sound_02;

    public void PlayRightSound() {
        sound_01.Play();
    }

    public void PlayLeftSound() {
        sound_02.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
