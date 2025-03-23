/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Interface that declares the handle for the state pattern to work.
 */

public interface IMovementState
{
    /// <summary>
    /// What happens when a state gets called
    /// </summary>
    /// <param name="movingObject">Object state is affecting</param>
    void Handle(MovingObject movingObject);
}
