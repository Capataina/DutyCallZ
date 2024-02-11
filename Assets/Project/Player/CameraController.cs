using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform playerCameraParent;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float recoilCorrectionSpeed;
    [SerializeField] float recoilSpeed;

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
        // print(deltaX);
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

        HandleRecoil();
    }

    void HandleRecoil()
    {
        if (Vector3.Magnitude(currentRecoilRotation - targetRecoilRotation) <= 0.05f)
        {
            if (targetRecoilRotation == Vector3.zero)
            {

                playerCameraParent.localRotation = Quaternion.identity;
                currentRecoilRotation = Vector3.zero;
            }
            else
            {
                currentRecoilRotation = targetRecoilRotation;
                targetRecoilRotation = Vector3.zero;
            }
        }
        else
        {
            var speed = targetRecoilRotation == Vector3.zero ? recoilCorrectionSpeed : recoilSpeed;
            // print(speed);
            currentRecoilRotation = Vector3.Lerp(currentRecoilRotation, targetRecoilRotation, speed * Time.deltaTime);
            playerCameraParent.localRotation = Quaternion.Euler(currentRecoilRotation);
        }

        //targetRecoilRotation = Vector3.Lerp(targetRecoilRotation, Vector3.zero, recoilCorrectionSpeed * Time.deltaTime);
        //currentRecoilRotation = Vector3.Slerp(currentRecoilRotation, targetRecoilRotation, recoilSpeed * Time.deltaTime);
        //playerCameraParent.localRotation = Quaternion.Euler(currentRecoilRotation);


    }

    public void AddRecoil(float xRecoil, float yRecoil)
    {
        targetRecoilRotation += new Vector3(-xRecoil, 0, 0) * Time.deltaTime;
    }
}
