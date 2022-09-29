using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class EagleMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _pathPoints;

    [SerializeField]
    private List<Transform> _points;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private PlayerMovement _player;

    [SerializeField]
    private float _velocityModifier;

    [SerializeField]
    private float _playerSearchRadius;

    private Vector2 _currentPlayerPosition;

    private Vector2 _currentPosition;

    private Vector2 _targetVector;

    private Vector2 _normalizedDirectionVector;

    private int _currentPoint;

    private bool _isEndOdPath;

    private bool _playerIsFar;

    private bool _isLookRight;

    private const float _pointReachabilityError = 0.1f;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        DeterminePlayerPosition();
        DetermineDistanceToPlayer();

        DeterminePosition();
        DetermineTargetVector();
        DetermineDirectionVector();

        if (_playerIsFar == true)
        {
            CheckEndOfPath();
            ChangePathPoint();
        }
        else
        {
            DetermineDistanceToNearestPathPoint();
        }

        ReflectScale();
    }

    private void FixedUpdate()
    {
        Fly();
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

        _playerIsFar = true;
    }

    private void DeterminePlayerPosition()
    {
        _currentPlayerPosition = _player.GetComponent<Rigidbody2D>().position;
    }

    private void DetermineDistanceToPlayer()
    {
        _playerIsFar = Vector2.Distance(_currentPosition, _currentPlayerPosition) > _playerSearchRadius;
    }

    private void DeterminePosition()
    {
        _currentPosition = _rigidbody2D.position;
    }

    private void DetermineTargetVector()
    {
        if (_playerIsFar == true)
        {
            _targetVector = _points[_currentPoint].position;
        }
        else
        {
            _targetVector = _currentPlayerPosition;
        }
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
        if (Mathf.Abs(_targetVector.x - _currentPosition.x) < _pointReachabilityError &&
            Mathf.Abs(_targetVector.y - _currentPosition.y) < _pointReachabilityError)
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

    private void DetermineDistanceToNearestPathPoint()
    {
        float distanceToNearestPathPoint = _playerSearchRadius;

        for (int i = 0; i < _points.Count; i++)
        {
            Vector2 pathPoint = _points[i].position;

            if (Vector2.Distance(_currentPosition, pathPoint) < distanceToNearestPathPoint)
            {
                distanceToNearestPathPoint = Vector2.Distance(_currentPosition, pathPoint);

                _currentPoint = i;
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

    private void Fly()
    {
        _rigidbody2D.velocity = _normalizedDirectionVector * _velocityModifier;
    }
}