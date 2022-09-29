using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class OpossumMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _pathPoints;

    [SerializeField]
    private List<Transform> _points;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private BoxCollider2D _boxCollider2D;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _velocityModifier;

    [SerializeField]
    private float _gravityModifier;

    private Vector2 _currentPosition;

    private Vector2 _targetVector;

    private Vector2 _normalizedDirectionVector;

    private int _currentPoint;

    private bool _isEndOdPath;

    private bool _isLookRight;

    private const float _pointReachabilityError = 0.1f;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        DeterminePosition();
        DetermineTargetVector();
        DetermineDirectionVector();

        CheckEndOfPath();
        ChangePathPoint();

        ReflectScale();
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }

    private void Initialize()
    {
        if (_pathPoints != null)
        {
            _points = new List<Transform>(_pathPoints.childCount);

            foreach (Transform point in _pathPoints)
            {
                _points.Add(point);
            }
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _isEndOdPath = false;
        _currentPoint = 0;

        _isLookRight = true;
    }

    private void DeterminePosition()
    {
        _currentPosition = _rigidbody2D.position;
    }

    private void DetermineTargetVector()
    {
        _targetVector = _points[_currentPoint].position;
    }

    private void DetermineDirectionVector()
    {
        _normalizedDirectionVector = (_targetVector - _currentPosition).normalized;
    }

    private void CheckEndOfPath()
    {
        if (_currentPoint == 0)
        {
            _isEndOdPath = false;
        }
        else if (_currentPoint == _points.Count - 1)
        {
            _isEndOdPath = true;
        }
    }

    private void ChangePathPoint()
    {
        if (Mathf.Abs(_targetVector.x - _currentPosition.x) < _pointReachabilityError)
        {
            if (_isEndOdPath == false)
            {
                _currentPoint++;
            }
            else
            {
                _currentPoint--;
            }
        }
    }

    private void ReflectScale()
    {
        if ((_isLookRight == true && _normalizedDirectionVector.x < 0) ||
            (_isLookRight == false && _normalizedDirectionVector.x > 0))
        {
            _spriteRenderer.flipX = _isLookRight;
            _isLookRight = !_isLookRight;
        }
    }

    private void Run()
    {
        if (_isLookRight == true)
        {
            _rigidbody2D.velocity = new Vector2(1f * _velocityModifier, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(-1f * _velocityModifier, _rigidbody2D.velocity.y);
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(-Vector2.up.normalized * _gravityModifier, ForceMode2D.Impulse);
    }
}