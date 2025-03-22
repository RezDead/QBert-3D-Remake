using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PurpleEgg : BaseEnemy
{
    private Vector3 _currPlayerPos;
    [SerializeField] private Mesh snakeMesh;
    
    protected override void AfterDescend()
    {
        GetComponentInChildren<MeshFilter>().mesh = snakeMesh;
        GetComponentInChildren<Transform>().localScale = new Vector3(0.25f, 1, 0.25f);
        killable = false;
        StartCoroutine(Chase());
    }
    
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
    
    private bool CheckIfValid()
    {
        RaycastHit hit;
        
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f);
    }

}
