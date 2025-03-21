using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovingObject
{
    private bool _moving = false;
    private PlayerControls playerControls;   
    
    private InputAction _upLeft, _upRight, _downLeft,_downRight;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _upLeft = playerControls.PlayerMovement.UpLeft;
        _upLeft.Enable();
        _upLeft.performed += UpLeft;
        
        _upRight = playerControls.PlayerMovement.UpRight;
        _upRight.Enable();
        _upRight.performed += UpRight;
        
        _downLeft = playerControls.PlayerMovement.DownLeft;
        _downLeft.Enable();
        _downLeft.performed += DownLeft;
        
        _downRight = playerControls.PlayerMovement.DownRight;
        _downRight.Enable();
        _downRight.performed += DownRight;
    }

    private void OnDisable()
    {
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

    private IEnumerator UpdateMovement()
    {
        _moving = true;
        yield return new WaitForSeconds(timeBetweenMovement);
        _moving = false;
    }

    private void CheckIfValid()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, .1f))
        {
            
        }
    }
}
