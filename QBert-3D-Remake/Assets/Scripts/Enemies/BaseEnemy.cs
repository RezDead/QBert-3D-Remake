using System.Collections;
using UnityEngine;

public class BaseEnemy : MovingObject
{
    public bool killable { get; private set; }
    public int score; 

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
    
}
