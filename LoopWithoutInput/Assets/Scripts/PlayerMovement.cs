using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed = 5f;
    public float jumpforce;
    public Transform groundcheck;
    public LayerMask groundObject;
    public float checkRadius;

	private Rigidbody2D rb;
	private bool facingRight = true;
	private float moveDir;
	public bool movement;
    private bool isJumping = false;
    public bool isGrounded;
    public static bool movementActive;

    private const float rotationDamp = 0.85f;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    // Use this for initialization
    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
        movementActive = true;
    }

	// Update is called once per frame
	void Update()
    {
        if(CountdownLvl1.movement || movementActive)
        {
            // UPDATE: Get Input if keys are pressed
            moveDir = Input.GetAxisRaw("HorizontalBoy"); // Scale of -1 --> 1
            Debug.Log(moveDir);
            if (Input.GetButtonDown("JumpBoy") && isGrounded)
            {
                isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, checkRadius, groundObject);

        if (CountdownLvl1.movement || movementActive)
        {
            // FIXEDUPDATE Pass INPUT information to Move-Method
            Move(moveDir * movementSpeed * Time.fixedDeltaTime, isJumping);
            isJumping = false;
        }
    }

    private void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, 0);

        //Actual movement
        rb.velocity = Vector3.SmoothDamp(rb.velocity, Quaternion.Euler(0, 0, rb.rotation) * targetVelocity * rotationDamp, ref m_Velocity, m_MovementSmoothing);

        //If the player pressed jumping
        if (isJumping)
        {
            Vector2 jumpVector2 = new Vector2(0f, jumpforce);
            rb.AddForce(Quaternion.Euler(0, 0, rb.rotation) * jumpVector2, ForceMode2D.Impulse);
        }

        //Flip Sprite depending on movement-direction
        if (moveDir > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDir < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

	private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; //you need to multiplicate it with -1 not 1 to flip the coords
        transform.localScale = theScale;
    }
}
