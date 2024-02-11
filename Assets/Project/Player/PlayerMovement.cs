using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed;
    public float speed;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;

        bool isGrounded = Physics.Raycast(playerController.transform.position, -transform.up, 1.05f, groundMask);

        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Quaternion lookDirectionQuaternion;
        if (lookDirection == Vector3.zero)
            lookDirectionQuaternion = Quaternion.identity;
        else
            lookDirectionQuaternion = Quaternion.LookRotation(lookDirection);

        movementDirection = lookDirectionQuaternion * movementDirection;

        if (!isGrounded)
        {
            movementDirection.y = -gravity;
        }

        playerController.Move(movementDirection.normalized * (speed * Time.deltaTime));
    }

    public bool isMoving()
    {
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        return movementVector.magnitude != 0;
    }
}
