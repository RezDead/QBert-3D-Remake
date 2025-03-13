using System.Collections;
using UnityEngine;

public class PurpleEgg : BaseEnemy
{
    public bool discHit = false;
    private Vector3 _currPlayerPos;
    
    protected override void AfterDescend()
    {
        StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        _currPlayerPos = LevelManager.instance.ReturnPlayerPosition();

        if (_currPlayerPos.z > transform.position.z)
        {
            Move(Direction.UpLeft);
        }
        
        else if (_currPlayerPos.z < transform.position.z)
        {
            Move(!lowerBorder ? Direction.DownRight : Direction.UpRight);
        }
        
        else if (_currPlayerPos.x > transform.position.x)   
        {
            Move(Direction.UpRight);
        }
        
        else if (_currPlayerPos.x < transform.position.x)
        {
            Move(!lowerBorder ? Direction.DownLeft : Direction.UpLeft);
        }
        
        yield return new WaitForSeconds(timeBetweenMoves);
        StartCoroutine(Chase());
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
