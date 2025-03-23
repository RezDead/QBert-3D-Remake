/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Handles the basic functionality for all enemies.
 */

using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseEnemy : MovingObject
{
    [SerializeField] protected bool killable;
    [SerializeField] protected int score; 

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Descend());
    }
    
    /// <summary>
    /// Basic descend that's shared for all enemies
    /// </summary>
    protected virtual IEnumerator Descend()
    {
        for (int timesDescended = 0; timesDescended < 5; timesDescended++)
        {
            yield return new WaitForSeconds(timeBetweenMovement);
            Move(Random.Range(0, 2) == 0 ? Direction.DownLeft : Direction.DownRight);
        }
        
        yield return new WaitForSeconds(timeBetweenMovement);
        
        AfterDescend();
    }

    /// <summary>
    /// What happens after descend, to be implemented in children
    /// </summary>
    protected virtual void AfterDescend()
    {
        
    }

    /// <summary>
    /// Checks if hit player and responds accordingly
    /// </summary>
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (killable)
        {
            Destroy(gameObject);
        }
        else
        {
            EventBus.Publish(GameEvents.PlayerDeath);
        }
    }
}
