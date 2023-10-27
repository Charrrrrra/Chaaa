using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ISceneManager : MonoBehaviour
{
    public static ISceneManager _instance;
    public int load_count = 0;
    void Awake() {
        if(_instance == null)
            _instance = this;
        else if(_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    
    public void load_StartScene() {
        load_count++;
        SceneManager.LoadScene(0);   
    }

    public void load_Scene02() {
        load_count++;
        SceneManager.LoadScene(1);
    }
}
