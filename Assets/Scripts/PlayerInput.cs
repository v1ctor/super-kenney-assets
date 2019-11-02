using UnityEngine;

[RequireComponent(typeof(Controller2D)), RequireComponent(typeof(Animator))]
public class PlayerInput : MonoBehaviour
{
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float moveSpeed = 6;

    public int jumpBufferingFrames = 4;
    public int coyoteTimeFrames = 4;

    

	private Controller2D controller;
	private Animator animator;

    private Vector2 input;

    private float gravity;
    private float jumpVelocity;
    private Vector2 velocity;
    private bool isGrounded;
    private bool isJumping;

    private int jumpFramesLeft;
    private int coyoteTimeLeft;
    

	// Use this for initialization
	void Start()
	{
		controller = GetComponent<Controller2D>();
		animator = GetComponent<Animator>();

		gravity = 2 * jumpHeight / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = gravity * timeToJumpApex;


	}

    void Update()
    {
        input = new Vector2(0, 0)
        {
            x = Input.GetAxisRaw("Horizontal"),
        };
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpFramesLeft = jumpBufferingFrames;
        }
    }

    void FixedUpdate()
	{
        bool wasGrounded = isGrounded;
        isGrounded = controller.collisions.below;
        if (wasGrounded && !isGrounded && !isJumping) {
            coyoteTimeLeft = coyoteTimeFrames;
        }
        isJumping &= !isGrounded;
        
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0f;
        }

        if (input.x > 0f)
		{
			// moving right
			if (transform.localScale.x < 0f)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			if (isGrounded)
			{
				animator.Play("Run");
			}
		}
		else if (input.x < 0f)
		{
			//moving left
			if (transform.localScale.x > 0f)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			if (isGrounded)
			{
				animator.Play("Run");
			}
		}
		else if (isGrounded)
		{
			animator.Play("Idle");
		}

		if ((isGrounded || coyoteTimeLeft > 0) && jumpFramesLeft > 0)
		{
			velocity.y = jumpVelocity;
            isJumping = true;
            animator.Play("Jump");
            jumpFramesLeft = 0;
            coyoteTimeLeft = 0;
		}
        if (jumpFramesLeft > 0) {
            jumpFramesLeft--;
        }

        if (coyoteTimeLeft > 0) {
            coyoteTimeLeft--;
        }
        


        float targetVelocityX = input.x * moveSpeed;

		velocity.x = targetVelocityX;
		velocity.y -= gravity * Time.deltaTime;
		var distance = velocity * Time.deltaTime;
		controller.Move(distance);

		velocity = controller.velocity;
	}
}