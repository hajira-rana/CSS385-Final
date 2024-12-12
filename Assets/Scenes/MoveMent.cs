using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMent : MonoBehaviour
{
    public InputCtrl playerInput;
    CharacterController characterController;
    Animator animator;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactor = 15f;
    public float speed = 3;
    Camera mainCamera;
    SeedMechanics seed;

    void Update()
    {
        if (!seed.isAiming)
        {
            movemove();
            animating();
            rotate();
            handleGravity();

            if (isRunPressed)
            {

                characterController.Move(currentRunMovement * Time.deltaTime * 30);

            }
            else
            {
                characterController.Move(currentMovement * Time.deltaTime * 30);
            }
        }

        handleGravity();
    }


    void Awake()
    {
        playerInput = new InputCtrl();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        seed = GetComponent<SeedMechanics>();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * speed;
        currentRunMovement.z = currentMovementInput.y * speed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }


    void movemove()
    {
        if (isMovementPressed)
        {
            mainCamera = Camera.main; // Get the main camera
            Vector3 cameraForward = mainCamera.transform.forward; // Get the forward vector of the camera
            Vector3 cameraRight = mainCamera.transform.right; // Get the right vector of the camera

            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize(); // Normalize the forward vector
            cameraRight.Normalize(); // Normalize the right vector
            handleGravity();

            // Calculate movement direction relative to the camera
            currentRunMovement = (cameraForward * currentMovementInput.y + cameraRight * currentMovementInput.x).normalized * speed;
            currentMovement = (cameraForward * currentMovementInput.y + cameraRight * currentMovementInput.x).normalized;
        }

    }



    void animating()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
        if (isMovementPressed && isRunPressed && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void waterAnim()
    {
        animator.speed = 2;
        animator.SetTrigger("Watering");

    }

    void rotate()
    {
        Vector3 lookPos = currentMovement;
        lookPos.y = 0.0f;
        Quaternion currentRot = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(currentRot, targetRot, rotationFactor * Time.deltaTime);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }



    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public PauseMenu pauseMenu;
    public void LoadPlayer()
    {
        PlayerInformation data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            pauseMenu.Resume();
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            Vector3 loadedPosition = new Vector3(data.position[0], data.position[1], data.position[2]);
            transform.position = loadedPosition;

            Debug.Log("Loaded position: " + loadedPosition);

            if (characterController != null)
            {
                characterController.enabled = true;
            }
        }
        else
        {
            Debug.LogError("No saved data found.");
        }
    }

}
