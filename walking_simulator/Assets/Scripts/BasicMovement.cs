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

    public bool isGround = false, isJump;

    public bool jumpPressed;
    int jumpCount;

    public Vector2 V;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);
    }

    void Update() {
        
        if (Input.GetButtonDown("Jump") && isGround) {
            jumpPressed = true;
        }

        GroundMovement();
        Jump();
        SwitchAnim();

        void GroundMovement() {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

            if (horizontalMove != 0) {
                transform.localScale = new Vector3(horizontalMove, 1, 1);
            }
        }

        void Jump() {
            if (isGround) {
                jumpCount = 1;
                isJump = false;
            }

            if(jumpPressed && isGround) {
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount--;
                jumpPressed = false;
                isGround = false;
            }
            
            Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);

            if(Physics2D.OverlapBox(boxCenter, boxSize, 0, ground) != null)
                isGround = true;
            else
                isGround = false;
        }

        V = rb.velocity;
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
}
