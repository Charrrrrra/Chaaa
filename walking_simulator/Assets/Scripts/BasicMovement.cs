using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isGround, isJump;

    public bool jumpPressed;

    public Vector2 V;

    public AudioSource jumpsound;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);

        can_move = true;

        transform.position = GameObject.Find("GeneratePoints").transform.position;
    }

    void GroundMovement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0) {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump() {
        if (isGround) 
            isJump = false;
        else
            isJump = true;

        if(jumpPressed && isGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false;
            isGround = false;
            jumpsound.Play();
        }
        
        Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);

        if(Physics2D.OverlapBox(boxCenter, boxSize, 0, ground) != null)
            isGround = true;
        else
            isGround = false;
    }

    void SwitchAnim() {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if(isGround) {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0) {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y <0) {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }
    
    void Update() {
        
        if (Input.GetButtonDown("Jump") && isGround) {
            jumpPressed = true;
        }

        if (can_move) {
            GroundMovement();
            Jump();
            SwitchAnim();
        }
        else
            anim.SetBool("walkout", true);

        V = rb.velocity;
    }
}
