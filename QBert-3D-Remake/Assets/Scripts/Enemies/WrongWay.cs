/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Class highlighting the unique functionality of the wrong way type enemy.
 */

using System.Collections;
using UnityEngine;

public class WrongWay : BaseEnemy
{
    public bool rightStart;
    
    /// <summary>
    /// Moves downwards randomly
    /// </summary>
    protected override IEnumerator Descend()
    {
        for (int timesDescended = 0; timesDescended < 8; timesDescended++)
        {
            yield return new WaitForSeconds(timeBetweenMovement);
            WrongMove(Random.Range(0, 2) == 0 ? Direction.DownLeft : Direction.DownRight);
        }
        
        yield return new WaitForSeconds(timeBetweenMovement);
        
        Destroy(this.gameObject);
    }
    
    private Vector3 _startPosition;
    private Vector3 _midPosition;
    private Vector3 _endPosition;
    private float _elapsedTime;
    
    private bool _moving;
    
    /// <summary>
    /// Altered movement to account for being sideways and what direction it started on, this function sets the positions
    /// </summary>
    private void WrongMove(Direction direction)
    {
        _moving = true;
        _elapsedTime = 0;
        _startPosition = transform.position;
        
        if (rightStart)
        {
            switch (direction)
            {
                case Direction.DownLeft:
                    _midPosition = _startPosition + new Vector3(-1, 0, 0);
                    _endPosition = _startPosition + new Vector3(-1, 0, 1);
                    break;
                case Direction.DownRight:
                    _midPosition = _startPosition + new Vector3(0, 1, 0);
                    _endPosition = _startPosition + new Vector3(0, 1, 1);
                    break;
            }
        }

        else
        {
            switch (direction)
            {
                case Direction.DownLeft:
                    _midPosition = _startPosition + new Vector3(0, 1, 0);
                    _endPosition = _startPosition + new Vector3(1, 1, 0);
                    break;
                case Direction.DownRight:
                    _midPosition = _startPosition + new Vector3(0, 0, -1);
                    _endPosition = _startPosition + new Vector3(1, 0, -1);
                    break;
            }
        }

    }
    
    /// <summary>
    /// Causes the movement
    /// </summary>
    private void Update()
    {
        if (!_moving) return;
        
        _elapsedTime += Time.deltaTime;
        float percentComplete = (_elapsedTime / timeBetweenMovement) * 2;

        if (percentComplete <= 1f)
        {
            transform.position = Vector3.Lerp(_startPosition, _midPosition, percentComplete);
        }
        else
        {
            transform.position = Vector3.Lerp(_midPosition, _endPosition, percentComplete - 1);
        }

        if (percentComplete >= 2)
        {
            _moving = false;
        }
    }
}
