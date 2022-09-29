using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player;

    [SerializeField]
    private float _smoothingMovements;

    [SerializeField]
    private Vector2 _offset;

    private const float _defaultSmoothingMovements = 7.5f;

    private const float _defaultOffsetX = 2.5f;
    private const float _defaultOffsetY = 3f;

    private void Reset()
    {
        _smoothingMovements = _defaultSmoothingMovements;

        _offset = new Vector2(_defaultOffsetX, _defaultOffsetY);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_player != null)
        {
            if (_player.IsLookRight == true)
            {
                ChangePosition(TargetPosition.Right);
            }
            else
            {
                ChangePosition(TargetPosition.Left);
            }
        }
    }

    private void ChangePosition(TargetPosition direction)
    {
        float x = 0f;

        if (direction == TargetPosition.Right)
        {
            x = _player.transform.position.x + _offset.x;
        }
        else if (direction == TargetPosition.Left)
        {
            x = _player.transform.position.x - _offset.x;
        }

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(x, _player.transform.position.y + _offset.y, transform.position.z),
            _smoothingMovements * Time.fixedDeltaTime);
    }

    private enum TargetPosition
    {
        Left,
        Right
    }
}