/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * State that causes the moving object to face a certain direction.
 */

using UnityEngine;

public class LookState : MonoBehaviour, IMovementState
{
    private MovingObject _movingObject;
    
    private Transform _childTransform;
    
    /// <summary>
    /// Gets the transform of the body of the object assuming it is the first child
    /// </summary>
    private void Start()
    {
        _childTransform = this.gameObject.transform.GetChild(0);
    }

    /// <summary>
    /// Rotates moving object to face a direction
    /// </summary>
    public void Handle(MovingObject movingObject)
    {
        if (!_movingObject)
        {
            _movingObject = movingObject;
        }
        
        switch (_movingObject.actionDirection)
        {
            case Direction.UpLeft:
                _childTransform.rotation = Quaternion.Euler(0, 270, 0);
                return;
            case Direction.UpRight:
                _childTransform.rotation = Quaternion.Euler(0, 0, 0);
                return;
            case Direction.DownLeft:
                _childTransform.rotation = Quaternion.Euler(0, 180, 0);
                return;
            case Direction.DownRight:
                _childTransform.rotation = Quaternion.Euler(0, 90, 0);
                return;
        }
    }
}
