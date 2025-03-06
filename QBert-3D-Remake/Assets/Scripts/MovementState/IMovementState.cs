public interface IMovementState
{
    /// <summary>
    /// What happens when a state gets called
    /// </summary>
    /// <param name="movingObject">Object state is affecting</param>
    void Handle(MovingObject movingObject);
}
