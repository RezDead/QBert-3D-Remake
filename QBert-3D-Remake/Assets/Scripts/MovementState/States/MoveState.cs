using UnityEngine;

public class MoveState : MonoBehaviour, IMovementState
{
    private MovingObject _movingObject;
    
    public void Handle(MovingObject movingObject)
    {
        if (!_movingObject)
        {
            _movingObject = movingObject;
        }

        switch (_movingObject.actionDirection)
        {
            case Direction.UpLeft:
                gameObject.transform.Translate(0, 1, 1);
                break;
            case Direction.UpRight:
                gameObject.transform.Translate(1, 1, 0);
                break;
            case Direction.DownLeft:
                gameObject.transform.Translate(-1, -1, 0);
                break;
            case Direction.DownRight:
                gameObject.transform.Translate(0, -1, -1);
                break;
        }
    }
}
