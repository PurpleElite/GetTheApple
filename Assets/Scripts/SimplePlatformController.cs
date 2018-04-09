using UnityEngine;

using System.Collections;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public float scale = 1f;
    public float velocity = 0f;
    public Transform groundCheck;


    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("RightArrow"))
        {
            if (scale < 1000)
                scale = scale + 100;
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
                anim.SetFloat("Speed", Mathf.Abs(h));

       
        if (h * rb2d.velocity.x < maxSpeed)
            velocity = rb2d.velocity.x;
            //rb2d.AddForce(Vector2.right * h * moveForce);
            rb2d.AddForce(Vector2.right * h * scale);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed/16, rb2d.velocity.y/4);

        if (h > 0 && facingRight)
            Flip();
        else if (h < 0 && !facingRight)
            Flip();

        if (jump)
        {
            
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, scale));
            jump = false;
            scale = 0f;
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