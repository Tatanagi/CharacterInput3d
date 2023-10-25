using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 movementInput;
    public float horizontalInput;
    public float verticalInput;
    public float moveAmount;
    public bool sprint_input;
    public bool walk_input;
    public bool run_input;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            
            //Sprinting
            playerControls.PlayerAction.Sprint.performed += i => sprint_input = true;
            playerControls.PlayerAction.Sprint.canceled += i => sprint_input = false;

            //Walking
            playerControls.PlayerAction.Walk.performed += i => walk_input = true;
            playerControls.PlayerAction.Walk.canceled += i => walk_input = false;

            //Running
            playerControls.PlayerAction.Running.performed += i => run_input = true;
            playerControls.PlayerAction.Running.canceled += i => run_input = false;

        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprinting();
        HandleWalking();
        HandleRunning();
       
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+ Mathf.Abs(verticalInput));
        PlayerManager.Instance.playerAnimation.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleSprinting()
    {
        if (sprint_input && moveAmount >= 0.5) 
        {  
            PlayerManager.Instance.isSprinting = true;
        }

        else
        {
            PlayerManager.Instance.isSprinting = false;
        }
    }
    private void HandleWalking()
    {
        if (walk_input && moveAmount <= 0.1)
        {
            PlayerManager.Instance.isWalking = true;
        }

        else
        {
            PlayerManager.Instance.isWalking = false;
        }
    }

    private void HandleRunning()
    {
        if (run_input && moveAmount > 0.3)
        {
            PlayerManager.Instance.isRunning = true;
        }

        else
        {
            PlayerManager.Instance.isRunning = false;
        }
    }


}
