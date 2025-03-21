using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slick : BaseEnemy
{
    protected override void AfterDescend()
    {
        Move(Random.Range(0, 2) == 0 ? Direction.DownLeft : Direction.DownRight);
        StartCoroutine(DestroyEgg());
    }

    private IEnumerator DestroyEgg()
    {
        yield return new WaitForSeconds(timeBetweenMovement);
        Destroy(gameObject);
    }
}
