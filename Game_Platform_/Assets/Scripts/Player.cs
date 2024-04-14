using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private BoxCollider2D checkWall;
    [SerializeField] private BoxCollider2D checkGround;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float acceleration;

    private Rigidbody2D rb;

    private BoxCollider2D boxCollider;

    private float inputHorizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();
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
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal != 0)
        {
            Flip();
            moveSpeed += acceleration * Time.fixedDeltaTime;
            moveSpeed = Mathf.Min(moveSpeed, 700f);
        }
        else
        {
            moveSpeed = 0f;
        }
        rb.velocity = new Vector2((moveSpeed * inputHorizontal) * Time.fixedDeltaTime, rb.velocity.y);
    }
    private void HandleJump()
    {

    }
    private void Flip()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * inputHorizontal, transform.localScale.y, transform.localScale.z);
    }
    private bool CheckWall()
    {
        return Physics2D.OverlapBox(checkWall.bounds.center, checkWall.bounds.size, 0f, wallMask);
    }
    private bool CheckGround()
    {
        return Physics2D.OverlapBox(checkGround.bounds.center, checkGround.bounds.size, 0f, groundMask);
    }
}
