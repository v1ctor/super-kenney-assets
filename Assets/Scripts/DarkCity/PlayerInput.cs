using UnityEngine;

[RequireComponent(typeof(Controller2D)), RequireComponent(typeof(Animator))]
public class PlayerInput : MonoBehaviour
{
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float moveSpeed = 6;

	private float jumpVelocity = 8;

	private Controller2D controller;
	private Animator animator;

	private float gravity = -20;
	private Vector2 velocity;

    private Vector2 input;
    private bool jumpPressed;

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
        input = new Vector2(0, 0);
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
	{
		bool grounded = controller.collisions.below;

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
			if (grounded)
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
			if (grounded)
			{
				animator.Play("Run");
			}
		}
		else if (grounded)
		{
			animator.Play("Idle");
		}

		if (grounded && jumpPressed)
		{
			velocity.y = jumpVelocity;
			animator.Play("Jump");
		}

		float targetVelocityX = input.x * moveSpeed;

		velocity.x = targetVelocityX;
		velocity.y -= gravity * Time.deltaTime;
		var distance = velocity * Time.deltaTime;
		controller.Move(distance);

		velocity = controller.velocity;
	}
}