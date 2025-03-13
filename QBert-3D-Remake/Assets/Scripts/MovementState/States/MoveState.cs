using UnityEngine;

public class MoveState : MonoBehaviour, IMovementState
{
    private MovingObject _movingObject;
    
    private Vector3 _startPosition;
    private Vector3 _midPosition;
    private Vector3 _endPosition;
    private float _elapsedTime;
    
    private bool _moving;
    
    public void Handle(MovingObject movingObject)
    {
        if (!_movingObject)
        {
            _movingObject = movingObject;
        }
        
        _moving = true;
        _elapsedTime = 0;
        _startPosition = _movingObject.transform.position;
        
        switch (_movingObject.actionDirection)
        {
            case Direction.UpLeft:
                _midPosition = _startPosition + new Vector3(0, 1, 0);
                _endPosition = _startPosition + new Vector3(0, 1, 1);
                break;
            case Direction.UpRight:
                _midPosition = _startPosition + new Vector3(0, 1, 0);
                _endPosition = _startPosition + new Vector3(1, 1, 0);
                break;
            case Direction.DownLeft:
                _midPosition = _startPosition + new Vector3(-1, 0, 0);
                _endPosition = _startPosition + new Vector3(-1, -1, 0);
                break;
            case Direction.DownRight:
                _midPosition = _startPosition + new Vector3(0, 0, -1);
                _endPosition = _startPosition + new Vector3(0, -1, -1);
                break;
        }
    }

    private void Update()
    {
        if (_moving)
        {
            _elapsedTime += Time.deltaTime;
            float percentComplete = (_elapsedTime / _movingObject.timeBetweenMovement) * 2;

            if (percentComplete <= 1f)
            {
                _movingObject.transform.position = Vector3.Lerp(_startPosition, _midPosition, percentComplete);
            }
            else
            {
                _movingObject.transform.position = Vector3.Lerp(_midPosition, _endPosition, percentComplete - 1);
            }

            if (percentComplete >= 2)
            {
                _moving = false;
            }
        }
    }
}

