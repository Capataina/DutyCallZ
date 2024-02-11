using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        var cameraEulerAngles = playerCamera.transform.localEulerAngles;

        float newRotationY = cameraEulerAngles.y + deltaX * mouseSensitivity;
        float newRotationX = cameraEulerAngles.x - deltaY * mouseSensitivity;

        playerCamera.transform.localRotation = Quaternion.Euler(newRotationX, newRotationY, 0);
    }
}
