using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        float deltaX = Input.GetAxisRaw("Mouse X");
        float deltaY = Input.GetAxisRaw("Mouse Y");

        var cameraEulerAngles = playerCamera.transform.eulerAngles;
        var playerEulerAngles = playerTransform.transform.eulerAngles;
        
        
        float newRotationY = playerEulerAngles.y + deltaX * Time.deltaTime * mouseSensitivity;
        float newRotationX = cameraEulerAngles.x - deltaY * Time.deltaTime * mouseSensitivity;
        
        if (newRotationX is < 270 and > 180)
        {
            newRotationX = 270;
        }
        else if (newRotationX is > 90 and < 180)
        {
            newRotationX = 90;
        }
        
        Quaternion newRotationCamera = Quaternion.Euler(newRotationX, newRotationY, 0);
        Quaternion newRotationPlayer = Quaternion.Euler(0, newRotationY, 0);
        
        playerCamera.transform.rotation = newRotationCamera;
        playerTransform.transform.rotation = newRotationPlayer;
    }
}
