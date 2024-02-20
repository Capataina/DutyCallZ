using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        speed = walkSpeed;
    }

    void Update()
    {

        Vector3 lookDirection = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;


        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // THIS WILL BREAK THE SPEED BUFF
        if (Input.GetKeyDown(KeyCode.C))
        {
            CrouchOrSlide(inputDirection, lookDirection);
        }

        if (isSliding && speed < 5f)
        {
            isSliding = false;
            isCrouching = true;
            speed = crouchSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isSliding && !isCrouching)
            {
                isSprinting = true;
                speed = sprintSpeed;
            }
        }
        else { isSprinting = false; }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCrouching = false;
            isSliding = false;
            ResetCollider();
        }

        if (!isSprinting && !isSliding && !isCrouching)
        {
            speed = walkSpeed;
        }


        bool isGrounded;
        if (isSliding || isCrouching)
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
        if (!isSliding)
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
            if (isJumping)
                isJumping = false;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCrouching)
            {
                isCrouching = false;
            }
            else
            {
                isJumping = true;
                verticalVelocity.y = jumpForce;
                cachedJumpSpeed = speed;
                isSliding = false;
            }
            ResetCollider();
        }

        if (isJumping)
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
