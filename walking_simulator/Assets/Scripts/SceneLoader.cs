using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    public bool inarea;
    public Animator animator;
    public GameObject text;
    public BasicMovement player;
    public AudioSource walkin;

    // // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);
        player = GameObject.Find("Cat").GetComponent<BasicMovement>();
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            inarea = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            inarea = false;
            text.SetActive(false);
        }
    }

    void LoadScene() {
        Debug.Log("0.0");
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (inarea == true) {
                animator.SetBool("FadeIn", true);
                animator.SetBool("FadeOut", false);
                walkin.Play();
                Invoke("LoadScene", 1);
                Debug.Log("yes");
                player.can_move = false;
            }
        }
    }
}
