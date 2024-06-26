using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    Holds all data related to the state machine
*/
public class StateMachine : MonoBehaviour
{
    StateFactory stateFactory;

    // Data
    [Header("Movement Speeds")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float sprintSpeed;
    [SerializeField] public float crouchSpeed;
    [SerializeField] public float slideSpeed;
    [SerializeField] public float slideHorizonalSpeed;
    [SerializeField] public float slideSpeedThreshold;
    [SerializeField] public float slidingDeceleration;
    [SerializeField] public float airStrafeSpeed;
    [Header("Jumping/Falling")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] public float jumpVelocity;
    [SerializeField] public float gravity;
    [SerializeField] public float terminalVelocity;
    [Header("Dependencies")]
    [SerializeField] public Animator cameraAnimationController;
    [SerializeField] public Animator playerAnimationController;
    [SerializeField] public CapsuleCollider playerCollider;
    [SerializeField] public GameObject playerModel;
    [SerializeField] public WeaponSway weaponRockController;
    [SerializeField] public CharacterController playerController;
    [SerializeField] public Camera playerCamera;

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

        RaycastHit temp;
        isGrounded = Physics.SphereCast(playerController.transform.position, 0.5f, -transform.up, out temp, 0.55f, groundMask);
    }

}
