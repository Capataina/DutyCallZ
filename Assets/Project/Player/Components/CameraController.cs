using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float zTiltStrength;
    [SerializeField] private float zTiltReturnSpeed;

    float currentTilt;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // manage mouse lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float deltaX = Input.GetAxisRaw("Mouse X");
        float deltaY = Input.GetAxisRaw("Mouse Y");

        // rotate y for player, x for camera
        var cameraEulerAngles = cameraAnchor.transform.localEulerAngles;
        var playerEulerAngles = playerTransform.transform.eulerAngles;

        float newRotationY = playerEulerAngles.y + deltaX * mouseSensitivity;
        float newRotationX = cameraEulerAngles.x - deltaY * mouseSensitivity;

        // tilt camera based on horizontal mouse swings
        currentTilt -= deltaX * zTiltStrength;

        if (newRotationX is < 270 and > 180)
        {
            newRotationX = 270;
        }
        else if (newRotationX is > 90 and < 180)
        {
            newRotationX = 90;
        }

        Quaternion newRotationCamera = Quaternion.Euler(newRotationX, 0, currentTilt);
        Quaternion newRotationPlayer = Quaternion.Euler(0, newRotationY, 0);

        cameraAnchor.transform.localRotation = newRotationCamera;
        playerTransform.transform.rotation = newRotationPlayer;

        // always reset the z-tilt slowly
        currentTilt = Mathf.Lerp(currentTilt, 0, zTiltReturnSpeed * Time.deltaTime);
    }
}
