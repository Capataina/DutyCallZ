using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    StateFactory stateFactory;

    // Data
    [SerializeField] public float walkSpeed;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float crouchSpeed;
    [SerializeField] public float slideSpeed;
    [SerializeField] public float slideHorizonalSpeed;
    [SerializeField] public float slideSpeedThreshold;
    [SerializeField] public float slidingDeceleration;
    [SerializeField] public float airStrafeSpeed;
    [SerializeField] public CharacterController playerController;
    [SerializeField] public Camera playerCamera;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] public float jumpVelocity;
    [SerializeField] public float gravity;
    [SerializeField] public float terminalVelocity;
    [SerializeField] public Animator cameraAnimationController;
    [SerializeField] public Animator playerAnimationController;
    [SerializeField] public CapsuleCollider playerCollider;
    [SerializeField] public GameObject playerModel;
    [SerializeField] public WeaponRock weaponRockController;

    [HideInInspector] public BaseState currentState;
    [HideInInspector] public Vector3 input;
    [HideInInspector] public Vector3 movementDirection;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public float lastGroundSpeed;
    [HideInInspector] public float slidingSpeed;
    [HideInInspector] public Vector3 slideDiretion;

    private void Start()
    {
        stateFactory = new StateFactory(this);
        currentState = stateFactory.Root();
    }

    private void Update()
    {
        ProcessData();
        currentState.BaseUpdate();
    }

    private void ProcessData()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 lookDirection = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;
        Quaternion lookDirectionQuaternion = Quaternion.LookRotation(lookDirection);
        movementDirection = lookDirectionQuaternion * input;

        isGrounded = Physics.Raycast(playerController.transform.position, -transform.up, 1.05f, groundMask);
    }

}
