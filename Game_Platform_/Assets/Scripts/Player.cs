using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float forceJump;
    [SerializeField] private BoxCollider2D checkGround;
    [SerializeField] private LayerMask groundMask;


    private Rigidbody2D rb;
    private Animator animator;

    private enum State { idle , run , jump , attack , hurt , die}
    private State state;

    private const string STATE = "state";
    private const string IS_GROUND = "isGround";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        Vector3 scale = transform.localScale;
        if (inputHorizontal != 0)
        {
            scale.x = Mathf.Abs(scale.x) * inputHorizontal;
            transform.localScale = scale;
            state = State.run;
        }
        else
        {
            state = State.idle;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && IsGround())
        {
            state = State.idle;
            rb.velocity = new Vector2(rb.velocity.x, forceJump);
        }
        rb.velocity = new Vector2(moveSpeed * inputHorizontal, rb.velocity.y);

        animator.SetInteger(STATE, (int)state);
        animator.SetBool(IS_GROUND, IsGround());
    }
    private bool IsGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(checkGround.bounds.center, checkGround.bounds.size, 0f, Vector2.down, 1f, groundMask);
        return hit.collider != null;
    }
}
