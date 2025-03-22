using System;
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
    
    protected virtual IEnumerator Descend()
    {
        for (int timesDescended = 0; timesDescended < 6; timesDescended++)
        {
            yield return new WaitForSeconds(timeBetweenMovement);
            Move(Random.Range(0, 2) == 0 ? Direction.DownLeft : Direction.DownRight);
        }
        
        yield return new WaitForSeconds(timeBetweenMovement);
        
        AfterDescend();
    }

    protected virtual void AfterDescend()
    {
        
    }

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
