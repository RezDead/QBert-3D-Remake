using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Direction actionDirection { get; private set; }
    public Direction currentDirection { get; private set; }
    private IMovementState _moveState, _lookState, _idleState;
    private MovementStateContext _context;
    
    /// <summary>
    /// Adds all the states to object and sets starting state to idle
    /// </summary>
    protected virtual void Start()
    {
        _context = new MovementStateContext(this);

        _idleState = gameObject.AddComponent<IdleState>();
        _moveState = gameObject.AddComponent<MoveState>();
        _lookState = gameObject.AddComponent<LookState>();
        
        _context.Transition(_idleState);
    }

    /// <summary>
    /// Makes the object move in a given direction
    /// </summary>
    /// <param name="direction">Direction to move in</param>
    public void Move(Direction direction)
    {
        actionDirection = direction;
        _context.Transition(_moveState);
    }
    
    /// <summary>
    /// Makes the object look towards a given direction
    /// </summary>
    /// <param name="direction">Direction to look at</param>
    public void Look(Direction direction)
    {
        actionDirection = direction;
        _context.Transition(_lookState);
    }
    
    /// <summary>
    /// Sets the current state to idle
    /// </summary>
    public void Idle()
    {
        _context.Transition(_idleState);
    }
}
