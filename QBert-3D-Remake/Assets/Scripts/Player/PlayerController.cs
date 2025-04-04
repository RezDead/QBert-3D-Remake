/*
 * Author: Kroeger-Miller, Julian
 * Last Updated: 03/22/2025
 * Script that handles the player controls and movement.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovingObject
{
    //Used specially for disc movement
    private bool _discMoving = false;
    private Vector3 _startPos;
    private Vector3 _midPos;
    private Vector3 _endPos;
    private float _elapsedTime;
    
    private bool _moving = false;
    private PlayerControls _playerControls;   
    
    private InputAction _upLeft, _upRight, _downLeft,_downRight;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(GameEvents.DiscUsed, DiscUsed);
        
        _upLeft = _playerControls.PlayerMovement.UpLeft;
        _upLeft.Enable();
        _upLeft.performed += UpLeft;
        
        _upRight = _playerControls.PlayerMovement.UpRight;
        _upRight.Enable();
        _upRight.performed += UpRight;
        
        _downLeft = _playerControls.PlayerMovement.DownLeft;
        _downLeft.Enable();
        _downLeft.performed += DownLeft;
        
        _downRight = _playerControls.PlayerMovement.DownRight;
        _downRight.Enable();
        _downRight.performed += DownRight;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvents.DiscUsed, DiscUsed);
        
        _upLeft.Disable();
        _upRight.Disable();
        _downLeft.Disable();
        _downRight.Disable();
    }

    private void UpLeft(InputAction.CallbackContext context)
    {
        if (_moving) return;
        Look(Direction.UpLeft);
        Move(Direction.UpLeft);
        StartCoroutine(UpdateMovement());
    }

    private void UpRight(InputAction.CallbackContext context)
    {
        if (_moving) return;
        Look(Direction.UpRight);
        Move(Direction.UpRight);
        StartCoroutine(UpdateMovement());
    }

    private void DownLeft(InputAction.CallbackContext context)
    {
        if (_moving) return;
        Look(Direction.DownLeft);
        Move(Direction.DownLeft);
        StartCoroutine(UpdateMovement());
    }

    private void DownRight(InputAction.CallbackContext context)
    {
        if (_moving) return;
        Look(Direction.DownRight);
        Move(Direction.DownRight);
        StartCoroutine(UpdateMovement());
    }

    /// <summary>
    /// Sets the movement positions
    /// </summary>
    private void DiscUsed()
    {
        _startPos = transform.position;
        _midPos = new Vector3(_startPos.x, LevelManager.instance.newWorldZero.y, _startPos.z);
        _endPos = LevelManager.instance.newWorldZero;
        _elapsedTime = 0;
        StartCoroutine(DiscMovement());
    }

    /// <summary>
    /// Updates the current conditions of the player to allow for disc movement
    /// </summary>
    private IEnumerator DiscMovement()
    {
        _moving = true;
        _discMoving = true;
        yield return new WaitForSeconds(LevelManager.instance.discUseTime);
        _discMoving = false;
        _moving = false;
    }

    /// <summary>
    /// Updates the movement status of the player
    /// </summary>
    private IEnumerator UpdateMovement()
    {
        _moving = true;
        yield return new WaitForSeconds(timeBetweenMovement);
        _moving = false;
        
        if (!CheckIfValid())
            EventBus.Publish(GameEvents.PlayerDeath);
    }

    /// <summary>
    /// Checks what ground the player landed on
    /// </summary>
    /// <returns>True if valid landing location, false if not</returns>
    private bool CheckIfValid()
    {
        RaycastHit hit;
        
        bool hitObj = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f);
        
        if (hitObj && hit.collider.CompareTag("Disc"))
        {
            EventBus.Publish(GameEvents.DiscUsed);
            hit.collider.GetComponentInParent<Disc>().DiscHit();
            return true;
        }
        
        return hitObj;
    }

    /// <summary>
    /// Used for disc movement, lerping between previously set positions
    /// </summary>
    private void Update()
    {
        if (!_discMoving) return;
        
        _elapsedTime += Time.deltaTime;
        float percentComplete = (_elapsedTime / LevelManager.instance.discUseTime) * 2;

        if (percentComplete <= 1f)
        {
            transform.position = Vector3.Lerp(_startPos, _midPos, percentComplete);
        }
        else
        {
            transform.position = Vector3.Lerp(_midPos, _endPos, percentComplete - 1);
        }

        if (percentComplete >= 2)
        {
            _moving = false;
        }
    }
}
