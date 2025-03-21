using System.Collections;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int roundsBeforeNextLevel = 0;
    
    [Header("Player Info")] [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] private float respawnTime = 4f;
    
    [Header("Enemy Prefabs")] [SerializeField]
    private GameObject redEgg;
    [SerializeField]private GameObject purpleEgg, wrongWay, slick;

    private GameObject[] _enemyArr;

    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    private void Start()
    {
        //Initialize _enemyArr
        _enemyArr[0] = redEgg; _enemyArr[1] = purpleEgg; _enemyArr[2] = wrongWay; _enemyArr[3] = slick;
        
        playerPrefab = Instantiate(playerPrefab, new Vector3(100, 100, 100), Quaternion.identity);
    }

    public IEnumerator PlayerDeath(bool deathByFalling)
    {
        StartCoroutine(ResetEnemies(!deathByFalling));
        playerPrefab.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
    }
    
    /// <summary>
    /// Raises the number of cubes completed, if all are completed ends round
    /// </summary>
    public void CubeCompleted()
    {
        _cubesCompleted++;
        
        //All cubes completed
        if (_cubesCompleted == TOTALCUBES)
        {
            EndRound();
        }
    }

    /// <summary>
    /// Reduces the number of cubes that are completed
    /// </summary>
    public void CubeReset()
    {
        _cubesCompleted--;
        
        //Something very wrong has occured
        if(_cubesCompleted < 0)
            Debug.LogWarning("Error with resetting cubes");
    }

    private void StartRound()
    {
        UpdateData();
    }
    
    private void EndRound()
    {
        StartCoroutine(ResetEnemies(true));
        StartRound();
    }

    private void UpdateData()
    {
        PlayerData.instance.currRound++;

        if (PlayerData.instance.currRound <= roundsBeforeNextLevel) return;
        
        PlayerData.instance.currRound = 0; PlayerData.instance.currLevel++;
    }
    
    public IEnumerator ResetEnemies(bool killPurple)
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        
    }

    public Vector3 ReturnPlayerPosition()
    {
        return playerPrefab.transform.position;
    }
    
}
