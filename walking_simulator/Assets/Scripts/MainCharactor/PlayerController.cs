using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
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

    public StateController sc {get; set;}
    public WalkState walking {get; set;}
    public IdleState idling {get; set;}
    public JumpState jumping {get; set;}
    public LeaveState leaving {get; set;}
    public SleepState sleeping {get; set;}
    

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;


        sc = new StateController();
        idling = new IdleState(this, sc);
        walking = new WalkState(this, sc);
        jumping = new JumpState(this, sc);
        leaving = new LeaveState(this, sc);
        sleeping = new SleepState(this, sc);

    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);

        can_move = true;

        transform.position = GameObject.Find("GeneratePoints").transform.position;
        SetCountText();



        sc.Initialize(idling);

    }

    void SetCountText() {
        switch_count_text.text = "Switch Count:" + ISceneManager._instance.load_count.ToString();
    }

    public void GroundMovement() {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0) {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    public bool GroundCheck() {

        Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);
        Collider2D colliderBox = Physics2D.OverlapBox(boxCenter, boxSize, 0, ground);
        ContactPoint2D[] contactPoints = new ContactPoint2D[10];
        
        if(colliderBox == null) return false;

        if(colliderBox.GetContacts(contactPoints) != 0) {
            return true;
        }
        else {
            return false;
        }

        // return Physics.Raycast(transform.position, Vector2.down, 20.0f);

        // return Physics.OverlapSphereNonAlloc(transform.position, 0.15f, colliders, ground) != 0;
    }

    public void Jump() {
        if(GroundCheck()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager._instance.PlayJumpSound();
        }
    }

    public void SwitchAnim() {
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
        

        sc.CurrentCharactorState.FrameUpdate();
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, 0.15f);
    // }
}
