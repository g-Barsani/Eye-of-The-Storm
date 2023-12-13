using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterMover characterMover;


    private Vector2 pointerInput, movementInput;
    //using lambda expression to make PointerInput a ReadOnly property
    public Vector2 PointerInput => pointerInput;

    private bool runInput =  false, dodgeInput = false;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, run, dodge;

    private void OnEnable()
    {
        //run.action.performed += Running;
        //run.action.canceled += RunningOff;
    }

    //OnDisable is when the object (Player) is disabled (not controlled)
    //private void OnDisable()
    //{
    //    run.action.performed -= Running;
    //    run.action.canceled -= RunningOff;
    //}

    private void Running(InputAction.CallbackContext context)
    {
        runInput = true;
        characterMover.isRunning = runInput;
    }

    private void RunningOff(InputAction.CallbackContext context)
    {
        runInput = false;
        characterMover.isRunning = runInput;
    }


    // Start is called before the first frame update
    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
    }

 

    // Update is called once per frame
    void Update()
    {
        //old way to do it
      //  movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //New way to do it using action and between '< >' we use the variable type
        movementInput = movement.action.ReadValue<Vector2>();

        pointerInput = GetPointerInput();

        characterMover.MovementInput = movementInput;

        run.action.performed += Running;
        run.action.canceled += RunningOff;

        dodge.action.performed += Dodging;
        dodge.action.canceled += DodgingOff;



        Debug.Log(movementInput);
    }

    private void DodgingOff(InputAction.CallbackContext context)
    {
        dodgeInput = false;
        characterMover.isDodging = dodgeInput;
    }

    private void Dodging(InputAction.CallbackContext context)
    {
        dodgeInput = true;
        characterMover.isDodging = dodgeInput;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        //Camera.main.ScreenToWorldPoint converts pixels from monitor to the game scene 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
