using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PurpleEgg : BaseEnemy
{
    public bool discHit = false;
    private Vector3 _currPlayerPos;
    private bool _xBorder = false, _zBorder = false, _lowerBorder = false;
    
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
            Move(!_lowerBorder ? Direction.DownRight : Direction.UpRight);
        }
        
        else if (_currPlayerPos.x > transform.position.x)   
        {
            Move(Direction.UpRight);
        }
        
        else if (_currPlayerPos.x < transform.position.x)
        {
            Move(!_lowerBorder ? Direction.DownLeft : Direction.UpLeft);
        }
        
        yield return new WaitForSeconds(timeBetweenMoves);
        StartCoroutine(Chase());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
        
        if (other.gameObject.CompareTag("XBorder"))
            _xBorder = true;
        
        if (other.gameObject.CompareTag("ZBorder"))
            _zBorder = true;
        
        if (other.gameObject.CompareTag("LowerBorder"))
            _lowerBorder = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("XBorder"))
            _xBorder = false;
        
        if (other.gameObject.CompareTag("ZBorder"))
            _zBorder = false;
        
        if (other.gameObject.CompareTag("LowerBorder"))
            _lowerBorder = false;
    }
}
