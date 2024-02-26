using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float slideSpeed;
    [SerializeField] private float slideDeceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CapsuleCollider playerCollider;

    [HideInInspector] public float speed;
    [HideInInspector] public bool isSprinting = false;
    [HideInInspector] public bool isCrouching = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public float activeSpeed;
    Vector3 slideDirection;
    Vector3 verticalVelocity;
    float cachedJumpSpeed;
    bool isJumping;

    public enum MovementStates
    {
        Walk,
        Crouch,
        Slide,
        Sprint,
        Airborne
    };

    [HideInInspector] public MovementStates currentMovementState = MovementStates.Walk;

    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {

        Vector3 lookDirection = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // THIS WILL BREAK THE SPEED BUFF

        switch (currentMovementState)
        {
            case MovementStates.Walk:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    speed = crouchSpeed;
                    CrouchCollider();
                    currentMovementState = MovementStates.Crouch;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = sprintSpeed;
                    currentMovementState = MovementStates.Sprint;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentMovementState = MovementStates.Airborne;
                    verticalVelocity.y = jumpForce;
                    cachedJumpSpeed = speed;
                    ResetCollider();
                }
                break;
            case MovementStates.Slide:
                print("sliding");
                if (speed < 4.5f)
                {
                    speed = crouchSpeed;
                    currentMovementState = MovementStates.Crouch;
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    speed = walkSpeed;
                    ResetCollider();
                    currentMovementState = MovementStates.Walk;
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    speed = sprintSpeed;
                    ResetCollider();
                    currentMovementState = MovementStates.Sprint;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentMovementState = MovementStates.Airborne;
                    verticalVelocity.y = jumpForce;
                    cachedJumpSpeed = speed;
                    ResetCollider();
                }
                break;
            case MovementStates.Sprint:
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    speed = walkSpeed;
                    currentMovementState = MovementStates.Walk;
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    speed = slideSpeed;
                    CrouchCollider();
                    currentMovementState = MovementStates.Slide;
                    slideDirection = (Quaternion.LookRotation(lookDirection.normalized) * inputDirection).normalized;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentMovementState = MovementStates.Airborne;
                    verticalVelocity.y = jumpForce;
                    cachedJumpSpeed = speed;
                    ResetCollider();
                }
                break;
            case MovementStates.Crouch:
                if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Space))
                {
                    speed = walkSpeed;
                    ResetCollider();
                    currentMovementState = MovementStates.Walk;
                }
                break;
            case MovementStates.Airborne:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    speed = slideSpeed;
                    CrouchCollider();
                    currentMovementState = MovementStates.Slide;
                    slideDirection = (Quaternion.LookRotation(lookDirection.normalized) * inputDirection).normalized;
                }
                break;
        }

        bool isGrounded;
        if (currentMovementState is MovementStates.Crouch or MovementStates.Slide)
        {
            isGrounded = Physics.Raycast(playerController.transform.position, -transform.up, .3f, groundMask);
        }
        else
        {
            isGrounded = Physics.Raycast(playerController.transform.position, -transform.up, 1.05f, groundMask);
        }

        Quaternion lookDirectionQuaternion;
        if (lookDirection == Vector3.zero)
            lookDirectionQuaternion = Quaternion.identity;
        else
            lookDirectionQuaternion = Quaternion.LookRotation(lookDirection);

        Vector3 movementDirection;
        if (currentMovementState != MovementStates.Slide)
        {
            movementDirection = lookDirectionQuaternion * inputDirection;
        }
        else
        {
            movementDirection = slideDirection;
            speed = Mathf.Lerp(speed, 0, slideDeceleration * Time.deltaTime);
        }

        if (!isGrounded)
        {
            verticalVelocity.y += -gravity * Time.deltaTime;
        }
        else if (verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0;
            if (currentMovementState == MovementStates.Airborne)
                currentMovementState = MovementStates.Walk;
        }

        if (currentMovementState == MovementStates.Airborne)
        {
            speed = cachedJumpSpeed;
        }

        playerController.Move(movementDirection.normalized * speed * Time.deltaTime + verticalVelocity);
    }

    private void CrouchOrSlide(Vector3 inputDirection, Vector3 lookDirection)
    {

        if (!isCrouching && !isSliding)
        {
            if (inputDirection.magnitude > 0)
            {
                isSliding = true;
                speed = slideSpeed;
                slideDirection = Quaternion.LookRotation(lookDirection) * inputDirection.normalized;
            }
            else
            {
                isCrouching = true;
                speed = crouchSpeed;
            }
            isSprinting = false;
            CrouchCollider();
            return;
        }
        else
        {
            isSliding = false;
            isCrouching = false;
            ResetCollider();
        }
    }

    private void CrouchCollider()
    {
        playerController.height = 1;
        playerController.center = new Vector3(0, 0.25f, 0);
        playerCollider.height = 1;
        playerCollider.center = new Vector3(0, 0.25f, 0);
    }

    private void ResetCollider()
    {
        playerController.height = 2;
        playerController.center = Vector3.zero;
        playerCollider.height = 2;
        playerCollider.center = Vector3.zero;
    }

    public bool isMoving()
    {
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return movementVector.magnitude != 0;
    }
}
