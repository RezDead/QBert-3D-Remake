using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Player Info")] [SerializeField]
    private GameObject playerPrefab;
    [SerializeField] private float respawnTime = 5f;
    public float discUseTime;
    
    private GameObject _player;
    
    [Header("Enemy Info")]
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private float enemySpawnRate = 5f;
    
    struct Enemy
    {
        public Enemy(GameObject enemyObject, EnemyTypes type)
        {
            this.enemyObject = enemyObject; this.type = type;
        }
        
        public GameObject enemyObject;
        public EnemyTypes type;
    }
    
    private List<Enemy> _currEnemies = new List<Enemy>();
    private bool _spawningEnemies = false;

    [HideInInspector]public int discsInScene = 0;
    private const int TOTALCUBES = 28;
    private int _cubesCompleted = 0;

    public readonly Vector3 newWorldZero = new Vector3(100, 100, 100);

    public override void Awake()
    {
        base.Awake();
        
        EventBus.Subscribe(GameEvents.StartRound, StartRound);
        EventBus.Subscribe(GameEvents.EndRound, EndRound);
        EventBus.Subscribe(GameEvents.DiscUsed, DiscUsed);
        EventBus.Subscribe(GameEvents.PlayerDeath, PlayerDeath);
        EventBus.Subscribe(GameEvents.NextLevel, NextLevel);
    }
    
    private void Start()
    {
        EventBus.Publish(GameEvents.NextLevel);
    }

    private void PlayerDeath()
    {
        _spawningEnemies = false;
        ResetEnemies(true);
        StartCoroutine(PlayerRespawn());
    }

    private void DiscUsed()
    {
        StartCoroutine(OnDiscUsed());
    }

    private IEnumerator OnDiscUsed()
    {
        _spawningEnemies = false;
        ResetEnemies(false);
        yield return new WaitForSeconds(discUseTime);
        if (!_spawningEnemies)
            StartCoroutine(SpawnEnemies());
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print("Next Level");
    }
    
    private IEnumerator PlayerRespawn()
    {
        Destroy(_player);
        yield return new WaitForSeconds(respawnTime);
        _player = Instantiate(playerPrefab, newWorldZero, Quaternion.identity);
    }
    
    /// <summary>
    /// Raises the number of cubes completed, if all are completed ends round
    /// </summary>
    public IEnumerator CubeCompleted()
    {
        _cubesCompleted++;
        print("Cube Completed: " + _cubesCompleted);
        //All cubes completed
        if (_cubesCompleted == TOTALCUBES)
        {
            Destroy(_player);
            yield return new WaitForNextFrameUnit();
            EventBus.Publish(GameEvents.EndRound);
        }
    }

    /// <summary>
    /// Reduces the number of cubes that are completed
    /// </summary>
    public void CubeReset()
    {
        _cubesCompleted--;
        
        //Something very wrong has occured :(
        if(_cubesCompleted < 0)
            Debug.LogWarning("Error with resetting cubes: " + _cubesCompleted);
    }

    private void StartRound()
    {
        if (_player != null)
        {
            Destroy(_player);
        }

        _player = Instantiate(playerPrefab, newWorldZero, Quaternion.identity);
        if (!_spawningEnemies)
            StartCoroutine(SpawnEnemies());
        _cubesCompleted = 0;
        print("New Round");
    }
    
    private void EndRound()
    {
        _spawningEnemies = false;
        ResetEnemies(true);
        print("End Round");
    }
    
    private void ResetEnemies(bool killPurple)
    {

        Enemy[] enemies = _currEnemies.ToArray();
        List<Enemy> tempCurrEnemies = new List<Enemy>();
        
        
        for (int enemiesProcessed = 0; enemiesProcessed < enemies.Length; enemiesProcessed++)
        {
            if (enemies[enemiesProcessed].type != EnemyTypes.PurpleEgg && !killPurple)
            {
                Destroy(enemies[enemiesProcessed].enemyObject);
            }

            else if (enemies[enemiesProcessed].type == EnemyTypes.PurpleEgg && !killPurple)
            {
                tempCurrEnemies.Add(enemies[enemiesProcessed]);
            }

            else
            {
                Destroy(enemies[enemiesProcessed].enemyObject);
            }
        }
        
        _currEnemies = tempCurrEnemies;
    }

    /// <summary>
    /// Spawns a random enemy based on how often enemy spawn rate is set to.
    /// </summary>
    private IEnumerator SpawnEnemies()
    {
        print("Spawning Enemies");
        
        _spawningEnemies = true;
        
        yield return new WaitForSeconds(enemySpawnRate);

        EnemyTypes enemySpawned = (EnemyTypes)Random.Range(0, 4);

        GameObject spawnedEnemyObj;

        switch (enemySpawned)
        {
            case EnemyTypes.RedEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[0], newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            case EnemyTypes.PurpleEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[1], newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.PurpleEgg));
                break;
            case EnemyTypes.WrongWay:
                if (Random.Range(0, 2) == 0)
                {
                    spawnedEnemyObj = Instantiate(enemyTypes[2], new Vector3(93, 93, 100), Quaternion.identity);
                    spawnedEnemyObj.GetComponent<WrongWay>().rightStart = false;
                    _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.WrongWay));
                }
                else
                {
                    spawnedEnemyObj = Instantiate(enemyTypes[2], new Vector3(100, 93, 93), Quaternion.identity);
                    spawnedEnemyObj.GetComponent<WrongWay>().rightStart = true;
                    _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.WrongWay));
                }
                break;
            case EnemyTypes.Slick:
                spawnedEnemyObj = Instantiate(enemyTypes[3], newWorldZero, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _spawningEnemies = false;
        
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Used to fetch players world position
    /// </summary>
    /// <returns>Players world position</returns>
    public Vector3 ReturnPlayerPosition()
    {
        return _player.transform.position;
    }
    
}
