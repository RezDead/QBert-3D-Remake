using UnityEngine;

public class IdleState : MonoBehaviour, IMovementState
{
    private MovingObject _movingObject;
    
    public void Handle(MovingObject movingObject)
    {
        if (!_movingObject)
        {
            _movingObject = movingObject;
        }
    }
}
