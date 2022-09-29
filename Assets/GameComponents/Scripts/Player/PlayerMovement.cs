using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _velocityModifier;

    [SerializeField]
    private float _jumpModifier;

    [SerializeField]
    private float _gravityModifier;

    [SerializeField]
    private LayerMask _groundLayerMask;

    [SerializeField]
    private Transform _ground;

    private Vector2 _movementVector;

    private bool _isGround;

    private bool _isFall;

    public bool IsLookRight { get; private set; }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ReadOfController();
        ChangeAnimation();
        ReflectScale();
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }

    private void Initialize()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        IsLookRight = true;
    }

    private void ReadOfController()
    {
        _movementVector.x = Input.GetAxisRaw(InputAxis.Params.Horizontal);
        _movementVector.y = Input.GetAxisRaw(InputAxis.Params.Vertical);
    }

    private void ChangeAnimation()
    {
        _animator.SetFloat(PlayerAnimatorController.Params.ParamMoveX, Mathf.Abs(_movementVector.x));
        _animator.SetBool(PlayerAnimatorController.Params.ParamIsGround, _isGround);
        _animator.SetBool(PlayerAnimatorController.Params.ParamIsFall, _isFall);
    }

    private void ReflectScale()
    {
        if ((IsLookRight == true && _movementVector.x < 0) || (IsLookRight == false && _movementVector.x > 0))
        {
            _spriteRenderer.flipX = IsLookRight;
            IsLookRight = !IsLookRight;
        }
    }

    private void Run()
    {
        _rigidbody2D.velocity = new Vector2(_movementVector.x * _velocityModifier, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        _isGround = Physics2D.Linecast(_rigidbody2D.position, _ground.position, _groundLayerMask) && _rigidbody2D.velocity.y == 0;

        if (_isGround == true && _movementVector.y > 0)
        {
            _rigidbody2D.AddForce(Vector2.up * _movementVector.y * _jumpModifier, ForceMode2D.Impulse);
        }

        _rigidbody2D.AddForce(-Vector2.up.normalized * _gravityModifier, ForceMode2D.Impulse);

        if (_rigidbody2D.velocity.y < 0)
        {
            _isFall = true;
        }
        else
        {
            _isFall = false;
        }
    }
}