using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player;

    [SerializeField]
    private float _smoothingMovements;

    [SerializeField]
    private Vector2 _offset;

    private void Reset()
    {
        _smoothingMovements = Constant.DefaultSmoothingMovements;

        _offset = new Vector2(Constant.DefaultOffsetX, Constant.DefaultOffsetY);
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

    private static class Constant
    {
        public const float DefaultSmoothingMovements = 7.5f;

        public const float DefaultOffsetX = 2.5f;
        public const float DefaultOffsetY = 3f;
    }
}