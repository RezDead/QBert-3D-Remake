public class MovementStateContext
{
    public IMovementState currentState { get; set; }
    private MovingObject _movingObject;
    
    private readonly IMovementState _movementState;

    public MovementStateContext(MovingObject movingObject)
    {
        _movingObject = movingObject;
    }
    
    /// <summary>
    /// Recalls the handle of the current state
    /// </summary>
    public void Transition()
    {
        currentState.Handle(_movingObject);
    }
    
    /// <summary>
    /// Changes the current state of movement to a given state
    /// </summary>
    /// <param name="newState">New state applied</param>
    public void Transition(IMovementState newState)
    {
        currentState = newState;
        currentState.Handle(_movingObject);
    }
}
