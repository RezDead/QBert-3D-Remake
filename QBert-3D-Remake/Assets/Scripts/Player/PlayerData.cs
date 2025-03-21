using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public int currentScore = 0;
    public int lives = 3;
    public int currLevel = 0;
    public int currRound = 0;
}
