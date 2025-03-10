using UnityEngine;

public class PlayerController : MovingObject
{
    private void OnGUI()
    {
        if (GUILayout.Button("UpLeft"))
        {
            Move(Direction.UpLeft);
        }
        if (GUILayout.Button("UpRight"))
        {
            Move(Direction.UpRight);
        }
        if (GUILayout.Button("DownLeft"))
        {
            Move(Direction.DownLeft);
        }
        if (GUILayout.Button("DownRight"))
        {
            Move(Direction.DownRight);
        }
        
    }
}
