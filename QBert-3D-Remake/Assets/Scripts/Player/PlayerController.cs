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
        
        if (GUILayout.Button("TurnUpLeft"))
        {
            Look(Direction.UpLeft);
        }
        if (GUILayout.Button("TurnUpRight"))
        {
            Look(Direction.UpRight);
        }
        if (GUILayout.Button("TurnDownLeft"))
        {
            Look(Direction.DownLeft);
        }
        if (GUILayout.Button("TurnDownRight"))
        {
            Look(Direction.DownRight);
        }
    }
}
