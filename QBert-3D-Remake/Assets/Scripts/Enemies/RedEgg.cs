/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Class highlighting the unique functionality of the red egg type enemy.
 */

using System.Collections;
using UnityEngine;

public class RedEgg : BaseEnemy
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
}
