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
	public AudioSource jumpSound;
	public AudioSource landSound;
	public AudioSource tumbleSound;
	public AudioSource faceSound;
    private float velocity = 0f;
    private BoxCollider2D groundCheck;
    public float jumpPower = 0f;
    private bool grounded = false;
    private bool control = true;
    private bool tumble = false;
    private bool recovering = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        Debug.Log("Test");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))

        {
            /* jumpPower = jumpPower + 20f;
             if (jumpPower == 300f)
             {
                 jump = true;
             }*/
            jump = true;
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

        //Determine if basketman is done getting up after a faceplant
        if (recovering)
        {
            Debug.Log("recovering");
            if (anim.GetCurrentAnimatorStateInfo(0).length <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                Debug.Log("recovered");
                anim.ResetTrigger("GetUp");
                recovering = false;
                control = true;
            }
        }

        //Determine what animation to play and whether the player currently has control
        if (recovering == false && rb2d.velocity.y < -1 * (terminalVelocity - 0.1))
        {
            anim.SetTrigger("Tumble");
            control = false;
        }
        else if (Mathf.Abs(rb2d.velocity.y) > 0.01)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            anim.SetTrigger("Faceplant");
            anim.ResetTrigger("Tumble");
            tumble = false;
        }
        else if (control == false && (Input.GetButton("Jump") || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01))
        {
            anim.SetTrigger("GetUp");
            recovering = true;
        }
        else if (recovering == false && control == true && Mathf.Abs(rb2d.velocity.y) > 0.01)
        {
            anim.SetTrigger("Jump");
        }
        else if (recovering == false && control == true && Mathf.Abs(rb2d.velocity.x) > 0.5)
        {
            anim.SetTrigger("Run");
        }
        else if (recovering == false && control == true)
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
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
		AudioSource[] audio = GetComponents<AudioSource>();
		jumpSound = audio[0];
		landSound = audio [1];
		tumbleSound = audio [2];
		faceSound = audio [3];
        currentJump = 0;

            print("you are holding JUMP");

            jumpPower = jumpPower + 5f;

            if (jumpPower >= 400f)

            {

                jump = true;

            }

            anim.ResetTrigger("Jump");
            tumble = true;
        }
        else if (recovering == false && tumble == true && Mathf.Abs(rb2d.velocity.y) < 0.01)
			jumpSound.Play();
            rb2d.AddForce(new Vector2(0f, jumpForce));