using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private LayerMask _playerMask;

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool isDoubleJump = false;

    private enum State { idle , walk ,jump}
    private State _state;

    private const string STATE = "State";
    private const string IS_GROUND = "IsGround";


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }
    private void HandleMovement()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal != 0 && IsGround())
        {
            _spriteRenderer.flipX = inputHorizontal > 0f ? false : true;
            _state = State.walk;
        }
        else
        {
            _state = State.idle;
        }
        _rb.velocity = new Vector2(inputHorizontal * _moveSpeed, _rb.velocity.y);
        _animator.SetInteger(STATE,(int)_state);
        _animator.SetBool(IS_GROUND, IsGround());
    }
    private void HandleJump()
    {
        Debug.Log(Input.GetKeyDown(KeyCode.Space));
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _forceJump);
        }
        /*else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isDoubleJump)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _forceJump);
                isDoubleJump = true;
            }
        }*/
        //_animator.SetInteger(STATE, (int)_state);
    }
    private bool IsGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f, _playerMask);
        return hit.collider != null;
    }
}
