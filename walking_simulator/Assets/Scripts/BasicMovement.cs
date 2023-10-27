using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Collider2D coll;
    public Animator anim;
    private Vector2 playerSize;
    private Vector2 boxSize;

    public float speed, jumpForce;
    public Transform groundCheck;
    public LayerMask ground;
    public float boxHeight = 0.5f;

    public bool can_move;
    public TextMeshProUGUI switch_count_text;
    public Transform UIposition;
    Transform target;
    Vector3 velocity = Vector3.zero;
    private Vector3 offset = new Vector3(0.08f, 0.2f, 0f);

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);

        can_move = true;

        transform.position = GameObject.Find("GeneratePoints").transform.position;
        SetCountText();
    }

    void SetCountText() {
        switch_count_text.text = "Switch Count:" + ISceneManager._instance.load_count.ToString();
    }

    void GroundMovement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0) {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    bool GroundCheck() {
        Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);

        if(Physics2D.OverlapBox(boxCenter, boxSize, 0, ground) != null)
            return true;
        else
            return false;
    }

    void Jump() {
        if(GroundCheck()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager._instance.PlayJumpSound();
        }
    }

    void SwitchAnim() {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if(GroundCheck()) {
            anim.SetBool("falling", false);
        }
        else if (!GroundCheck() && rb.velocity.y > 0) {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y <0) {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

    private void PlayRight() {
        AudioManager._instance.PlayRightSound();
    }
    
    private void PlayLeft() {
        AudioManager._instance.PlayLeftSound();
    }
    void Update() {
        Vector3 targetPosition = target.position;
        UIposition.position = targetPosition + offset;
        
        if (Input.GetButtonDown("Jump") && GroundCheck() && can_move) 
            Jump();

        if (can_move) {
            GroundMovement();
            SwitchAnim();
        }
        else
            anim.SetBool("walkout", true);
    }
}
