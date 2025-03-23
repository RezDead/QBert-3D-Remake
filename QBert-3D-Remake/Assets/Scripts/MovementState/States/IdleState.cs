/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * State that causes the moving object to be idle.
 */

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
