using System.Collections;
using UnityEngine;

public class PurpleEgg : BaseEnemy
{
    public bool discHit = false;
    private Vector3 _currPlayerPos;
    [SerializeField] private Mesh snakeMesh;
    
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void AfterDescend()
    {

        GetComponentInChildren<MeshFilter>().mesh = snakeMesh;
        GetComponentInChildren<Transform>().localScale = new Vector3(0.25f, 1, 0.25f);
        StartCoroutine(Chase());
    }
    
    

    // ReSharper disable Unity.PerformanceAnalysis
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
        StartCoroutine(Chase());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

    private IEnumerator WaitThenDestroy()
    {
        yield return new WaitForSeconds(timeBetweenMovement);
        
    }
}
