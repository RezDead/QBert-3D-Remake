using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "LevelObjects/Level", order = 1)]
public class LevelSettings : ScriptableObject
{ 
    public CubeSettings cubeSettings;
    
    [Range(1,3)]
    public int numHitsPerCube;
    
    [Range(0,100)]
    public int levelNumber;
    
}
