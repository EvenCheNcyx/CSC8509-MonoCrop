using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed;
    
    private float inputX;
    
    private float inputY;

    private Vector2 movementInput;

    private Animator[] animators;

    private bool IsMoving;

    private bool inputDisable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition += OnMoveToPosition;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
    }

    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    private void OnAfterSceneLoadedEvent()
    {
        inputDisable = false;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        inputDisable = true;
        //movementInput = new Vector2(0, 0);
    }

    private void Update()
    {
        if (!inputDisable)
            PlayerInput();
        else
            IsMoving = false;
        SwitchAnimation();
        
        //Debug.Log("inputdisable"+inputDisable+"ismoving"+IsMoving);
    }

    private void FixedUpdate()
    {
        if (!inputDisable) 
            Movement();
    }

    private void PlayerInput()
    {
        //if (inputY == 0) 
        inputX = Input.GetAxisRaw("Horizontal");
        //if (inputX == 0) 
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 && inputY != 0)
        {
            inputX = inputX * 0.6f;
            inputY = inputY * 0.6f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX * 2.0f;
            inputY = inputY * 2.0f;
        }
        movementInput = new Vector2(inputX, inputY);

        IsMoving = movementInput != Vector2.zero;
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.deltaTime);
    }

    private void SwitchAnimation()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("IsMoving",IsMoving);
            if (IsMoving)
            {
                anim.SetFloat("InputX",inputX);
                anim.SetFloat("InputY",inputY);
            }
        }
    }


}
