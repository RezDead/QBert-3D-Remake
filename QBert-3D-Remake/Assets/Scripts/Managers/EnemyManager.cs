/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Handles all things related to enemy spawning and despawning.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : Singleton<EnemyManager>
{
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
    private bool _isSpawning = false;

    /// <summary>
    /// Starts the spawning process
    /// </summary>
    public void StartSpawning()
    {
        if (_isSpawning) return;

        StartCoroutine(SpawnEnemies());
    }
    
    /// <summary>
    /// Stops any ongoing spawning
    /// </summary>
    public void StopSpawning()
    {
        if (!_isSpawning) return;

        StopAllCoroutines();
        _isSpawning = false;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// Spawns a random enemy based on how often enemy spawn rate is set to.
    /// </summary>
    private IEnumerator SpawnEnemies()
    {
        print("Spawning Enemies");
        
        _isSpawning = true;
        
        yield return new WaitForSeconds(enemySpawnRate);

        EnemyTypes enemySpawned = (EnemyTypes)Random.Range(0, 4);

        GameObject spawnedEnemyObj;

        Vector3 spawnLocation;

        if (Random.Range(0, 2) == 0)
        {
            spawnLocation = LevelManager.instance.newWorldZero - new Vector3(0, 1, 1);
        }
        else
        {
            spawnLocation = LevelManager.instance.newWorldZero - new Vector3(1, 1, 0);
        }

        switch (enemySpawned)
        {
            case EnemyTypes.RedEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[0], spawnLocation, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            case EnemyTypes.PurpleEgg:
                spawnedEnemyObj = Instantiate(enemyTypes[1], spawnLocation, Quaternion.identity);
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
                spawnedEnemyObj = Instantiate(enemyTypes[3], spawnLocation, Quaternion.identity);
                _currEnemies.Add(new Enemy(spawnedEnemyObj, EnemyTypes.RedEgg));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _isSpawning = false;
        
        StartCoroutine(SpawnEnemies());
    }
    
    /// <summary>
    /// Kills all enemies in the level
    /// </summary>
    /// <param name="killPurple">If true also kills the purple enemies</param>
    public void ResetEnemies(bool killPurple)
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
}
