using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public int currentJump;
    public float moveForce = 30f;
    public float maxSpeed = 2f;
    public float jumpForce = 250f;
    public float terminalVelocity = 5f;
    private float velocity = 0f;
    private BoxCollider2D groundCheck;
    public float jumpPower = 0f;
    private bool grounded = false;
    private bool control = true;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        Debug.Log("Test");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currentJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))

        {

            print("you are holding JUMP");

            jumpPower = jumpPower + 5f;

            if (jumpPower >= 400f)

            {

                jump = true;

            }

        }

        if (Input.GetButtonUp("Jump"))
        {
            jump = true;
        }


    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h * rb2d.velocity.x < maxSpeed && control)
        {
            velocity = rb2d.velocity.x;
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (Mathf.Abs(rb2d.velocity.y) > terminalVelocity)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * terminalVelocity);

        if (h > 0 && !facingRight && control)
            Flip();
        else if (h < 0 && facingRight && control)
            Flip();

        //Determine what animation to play and whether the player currently has control
        if (rb2d.velocity.y < -1 * (terminalVelocity-0.1) )
        {
            anim.SetTrigger("Tumble");
            control = false;
        }
        else if (Mathf.Abs(rb2d.velocity.y) > 0.01)
        {
            anim.SetTrigger("Jump");
            control = true;
        }
        else if (Mathf.Abs(rb2d.velocity.x) > 0.5)
        {
            anim.SetTrigger("Run");
            control = true;
        }
        else
        {
            anim.SetTrigger("Idle");
            control = true;
        }

        if (jump)
        {

            rb2d.AddForce(new Vector2(0f, jumpPower));

            jump = false;
            jumpPower = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("TriggerEnter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Enter");
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("TriggerExit");
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Exit");
            grounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}