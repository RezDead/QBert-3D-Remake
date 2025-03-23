/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Class highlighting the unique functionality of the purple egg type enemy.
 */

using System.Collections;
using UnityEngine;

public class PurpleEgg : BaseEnemy
{
    private Vector3 _currPlayerPos;
    [SerializeField] private Mesh snakeMesh;
    
    /// <summary>
    /// Changes from egg to snake
    /// </summary>
    protected override void AfterDescend()
    {
        GetComponentInChildren<MeshFilter>().mesh = snakeMesh;
        GetComponentInChildren<Transform>().localScale = new Vector3(0.25f, 1, 0.25f);
        killable = false;
        StartCoroutine(Chase());
    }
    
    /// <summary>
    /// Causes the snake the chase the player and fall off if need be
    /// </summary>
    private IEnumerator Chase()
    {
        _currPlayerPos = LevelManager.instance.ReturnPlayerPosition();
        
        // Player Z > Egg, same X
        if (_currPlayerPos.z > transform.position.z && Mathf.Approximately(_currPlayerPos.x, transform.position.x))
        {
            if(zBorder){}
            Move(Direction.UpLeft);
        }
        
        //Player X > Egg, same Z
        else if (_currPlayerPos.x > transform.position.x && Mathf.Approximately(_currPlayerPos.z, transform.position.z))
        {
            Move(Direction.UpRight);
        }
        
        //Player Z < Egg, same X
        else if (_currPlayerPos.z < transform.position.z && Mathf.Approximately(_currPlayerPos.x, transform.position.x))
        {
            Move(Direction.DownRight);
        }
        
        //Player X < Egg, same Z
        else if (_currPlayerPos.x < transform.position.x && Mathf.Approximately(_currPlayerPos.z, transform.position.z))
        {
            Move(Direction.DownLeft);
        }
        
        //Player Z > Egg
        else if (_currPlayerPos.z > transform.position.z)
        {
            Move(Direction.UpLeft);
        }
        
        //Player Z < Egg
        else if (_currPlayerPos.z < transform.position.z)
        {
            Move(!lowerBorder ? Direction.DownRight : Direction.UpRight);
        }
        
        //Player X > Egg
        else if (_currPlayerPos.x > transform.position.x)   
        {
            Move(Direction.UpRight);
        }
        
        //Player X < Egg
        else if (_currPlayerPos.x < transform.position.x)   
        {
            Move(!lowerBorder ? Direction.DownLeft : Direction.UpLeft);
        }
        
        
        yield return new WaitForSeconds(timeBetweenMovement);

        if (!CheckIfValid())
        {
            PlayerData.instance.currentScore += score;
            Destroy(gameObject);
        }
        
        StartCoroutine(Chase());
    }
    
    /// <summary>
    /// Checks if landed on a valid floor
    /// </summary>
    /// <returns>True if landed on a valid floor, false if not</returns>
    private bool CheckIfValid()
    {
        RaycastHit hit;
        
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f);
    }

}
