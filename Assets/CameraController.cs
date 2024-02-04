using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity;
    private float previousMousePosX;
    private float previousMousePosY;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        previousMousePosX = Input.mousePosition.x;
        previousMousePosY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        Vector3 currentPosition = Input.mousePosition;
        float deltaX = Input.GetAxisRaw("Mouse X");
        float deltaY = Input.GetAxisRaw("Mouse Y");

        var eulerAngles = playerCamera.transform.eulerAngles;
        float newRotationX = eulerAngles.y + deltaX * Time.deltaTime * mouseSensitivity;
        float newRotationY = eulerAngles.x - deltaY * Time.deltaTime * mouseSensitivity;
        
        if (newRotationY is < 270 and > 180)
        {
            newRotationY = 270;
        }
        else if (newRotationY is > 90 and < 180)
        {
            newRotationY = 90;
        }
        
        // 90 to 0 - 360 to 280 270
        
        Quaternion newRotationQuaternion = Quaternion.Euler(newRotationY, newRotationX, 0);
        
        playerCamera.transform.rotation = newRotationQuaternion;

        previousMousePosX = currentPosition.x;
        previousMousePosY = currentPosition.y;
    }
}
