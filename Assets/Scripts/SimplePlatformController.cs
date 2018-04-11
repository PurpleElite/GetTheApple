using UnityEngine;

using System.Collections;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 30f;
    public float maxSpeed = 2f;
    public float jumpForce = 250f;
    public float terminalVelocity = 5f;
	public AudioSource jumpSound;
	public AudioSource landSound;
	public AudioSource tumbleSound;
	public AudioSource faceSound;
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
		AudioSource[] audio = GetComponents<AudioSource>();
		jumpSound = audio[0];
		landSound = audio [1];
		tumbleSound = audio [2];
		faceSound = audio [3];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && grounded && control)
        {
            /* jumpPower = jumpPower + 20f;
             if (jumpPower == 300f)
             {
                 jump = true;
             }*/
            jump = true; 
			//jumpSound.PlayOneShot (jumpSound);
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
            //rb2d.AddForce(Vector2.right * h * scale);

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
			//tumbleSound.Play ();
            control = false;
			//if (rb2d.velocity.y == 0)
				//tumbleSound.Pause();
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
			jumpSound.Play();
            rb2d.AddForce(new Vector2(0f, jumpForce));

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