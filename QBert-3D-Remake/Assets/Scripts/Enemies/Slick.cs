/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Class highlighting the unique functionality of the slick type enemy.
 */

using System.Collections;
using UnityEngine;

public class Slick : BaseEnemy
{
    /// <summary>
    /// Randomly chooses a direction to move downwards
    /// </summary>
    protected override void AfterDescend()
    {
        Move(Random.Range(0, 2) == 0 ? Direction.DownLeft : Direction.DownRight);
        StartCoroutine(DestroyEgg());
    }

    /// <summary>
    /// Destroys eggs once it falls off
    /// </summary>
    private IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(timeBetweenMovement);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Redoes the collision to include on kill add score
    /// </summary>
    protected override void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (killable)
        {
            PlayerData.instance.currentScore += score;
            Destroy(gameObject);
        }
        else
        {
            EventBus.Publish(GameEvents.PlayerDeath);
        }
    }
}
