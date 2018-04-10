using UnityEngine;

using System.Collections;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 30f;
    public float maxSpeed = 2f;
    public float jumpForce = 250f;
    private bool grounded = false;
    private float velocity = 0f;
    private Transform groundCheck;

 
    public float jumpPower = 0f;




    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {

        groundCheck = transform.Find("groundCheck");

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpPower = jumpPower + 20f;
            if (jumpPower == 300f)
            {
                jump = true;
            }

        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            velocity = rb2d.velocity.x;
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();



        if (Mathf.Abs(rb2d.velocity.y) > 0.01)
        {
            anim.SetTrigger("Jump");

        }
        else if (Mathf.Abs(rb2d.velocity.x) > 0.5)
        {
            anim.SetTrigger("Run");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
        if (jump)
        {

            rb2d.AddForce(new Vector2(0f, jumpForce));

            jump = false;
            jumpPower = 0;
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