using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "NewRound", menuName = "LevelObjects/Round", order = 2)]
public class RoundSettings : ScriptableObject
{ 
    public EnemyTypes[] enemyTypes;
    
    [Range(0, 4)]
    public int numDiscs;
}