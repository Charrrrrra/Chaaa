using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    public AudioSource jumpsound;
    public AudioSource step_sound_01;
    public AudioSource step_sound_02;

    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayJumpSound() {
        jumpsound.Play();
    }

    public void PlayRightSound() {
        step_sound_01.Play();
    }

    public void PlayLeftSound() {
        step_sound_02.Play();
    }

}
