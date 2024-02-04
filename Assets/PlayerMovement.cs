using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up).normalized;

        bool isGrounded = Physics.Raycast(playerController.transform.position, -transform.up, 1.05f, groundMask);
        
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));

        Quaternion lookDirectionQuaternion = Quaternion.LookRotation(lookDirection);

        movementDirection = lookDirectionQuaternion * movementDirection;

        if (!isGrounded)
        {
            movementDirection.y = -gravity;
        }
        
        playerController.Move(movementDirection.normalized * (speed * Time.deltaTime));
    }
}
