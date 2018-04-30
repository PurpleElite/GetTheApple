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
    private AudioSource jumpSound;
    private AudioSource landSound;
    private AudioSource tumbleSound;
    private AudioSource faceSound;
    private AudioSource chargeSound;
    private float velocity = 0f;
    private BoxCollider2D groundCheck;
    [SerializeField]
    private float jumpPower = 100f;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private bool control = true;
    [SerializeField]
    private bool tumble = false;
    [SerializeField]
    private bool recovering = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    private GameObject caterpillar;

    //Keep track of number of objects the groundcheck has entered
    private int colliders = 0;


    // Use this for initialization
    void Awake()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        jumpSound = audio[0];
        tumbleSound = audio[1];
        faceSound = audio[2];
        chargeSound = audio[3];
        currentJump = 0;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        caterpillar = GameObject.FindGameObjectWithTag("CaterpillarManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && grounded && !recovering && !tumble && control)

        {
            jumpPower = jumpPower + 3f;
            if(!chargeSound.isPlaying)
                chargeSound.Play();
            if (jumpPower >= 400f)
            {
                jump = true;
            }
        }

        if (Input.GetButtonUp("Jump") && grounded && !recovering && !tumble && control)
        {
            chargeSound.Stop();
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
            if (anim.GetCurrentAnimatorStateInfo(0).length <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                anim.ResetTrigger("GetUp");
                recovering = false;
                control = true;
            }
        }

        //Determine what animation to play and whether the player currently has control
        if (recovering == false && rb2d.velocity.y < -1 * (terminalVelocity - 0.1))
        {
            chargeSound.Stop();
            anim.SetTrigger("Tumble");
            anim.ResetTrigger("Jump");
            control = false;
            tumble = true;
            
            if (!tumbleSound.isPlaying)
                tumbleSound.Play();
        }
        else if (recovering == false && tumble == true && Mathf.Abs(rb2d.velocity.y) < 0.01)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            anim.SetTrigger("Faceplant");
            anim.ResetTrigger("Tumble");
            tumble = false;
            tumbleSound.Stop();
            faceSound.Play();

            //Inform the caterpillar manager that a faceplant has occured
            caterpillar.GetComponent<CaterpillarManager>().FacePlant();
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
            //Inform the caterpillar manager that a jump has occured
            caterpillar.GetComponent<CaterpillarManager>().Jump(jumpPower);

            rb2d.AddForce(new Vector2(0f, jumpPower));
            jumpSound.Play();
            jump = false;
            jumpPower = 100f;
            chargeSound.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            colliders++;
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            colliders--;
            if(colliders == 0)
            {
                chargeSound.Stop();
                jumpPower = 100f;
                grounded = false;
            }
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
		

           

        