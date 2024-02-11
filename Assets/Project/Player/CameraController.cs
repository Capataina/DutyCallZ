using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;

    Vector3 targetRecoilRotation;
    Vector3 currentRecoilRotation;

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

        float deltaX = Input.GetAxisRaw("Mouse X");
        float deltaY = Input.GetAxisRaw("Mouse Y");

        var cameraEulerAngles = playerCamera.transform.localEulerAngles;
        var playerEulerAngles = playerTransform.transform.eulerAngles;

        float newRotationY = playerEulerAngles.y + deltaX * mouseSensitivity;
        float newRotationX = cameraEulerAngles.x - deltaY * mouseSensitivity;

        if (newRotationX is < 270 and > 180)
        {
            newRotationX = 270;
        }
        else if (newRotationX is > 90 and < 180)
        {
            newRotationX = 90;
        }

        Quaternion newRotationCamera = Quaternion.Euler(newRotationX, 0, 0);
        Quaternion newRotationPlayer = Quaternion.Euler(0, newRotationY, 0);

        if (deltaX != 0 || deltaY != 0)
        {
            playerCamera.transform.localRotation = newRotationCamera;
            playerTransform.transform.rotation = newRotationPlayer;
        }

    }

}
