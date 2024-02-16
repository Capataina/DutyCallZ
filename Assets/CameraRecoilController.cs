using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraRecoilController : MonoBehaviour
{

    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerCameraParent;
    [SerializeField] Transform cameraShakeParent;
    [SerializeField] WeaponsClass currentWeapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float maxReturnHeight;
    [SerializeField] float cameraShakeReturnSpeed;
    [SerializeField] float cameraShakeSpeed;
    [SerializeField] float zShapeFaze;

    float time;
    float duration;
    float height;
    float shakeTime;
    float currentStrength;

    Vector3 targetRotation;
    Vector3 currentRotation;

    public void AddRecoil(float xRecoil, float duration)
    {
        // Vertical Recoil
        height = xRecoil;
        this.duration = duration;
        time = duration;
    }

    public void AddCameraShake(float XShakeStrengh, float YShakeStrengh, float ZShakeStrength)
    {
        currentStrength = ZShakeStrength;
    }

    void HandleVerticalRecoil()
    {
        if (time > 0)
        {
            float rotAmount = -height * Time.deltaTime / duration;
            float rotX = playerCameraParent.localEulerAngles.x;
            float negataiveX = (rotX > 180) ? rotX - 360 : rotX;
            if (Mathf.Abs(negataiveX) > maxReturnHeight)
            {
                playerCamera.Rotate(rotAmount, 0, 0, Space.Self);
            }
            else
            {
                playerCameraParent.Rotate(rotAmount, 0, 0, Space.Self);
            }
            time -= Time.deltaTime;
        }
        else if (playerCameraParent.localEulerAngles.sqrMagnitude > 0.01f)
        {
            //currentReturnForce = returnSpeed;
            playerCameraParent.localRotation = Quaternion.Slerp(playerCameraParent.localRotation, Quaternion.identity, returnSpeed * Time.deltaTime);
            //recoilActive = false;
        }
    }

    void HandleCameraShake()
    {
        currentStrength = Mathf.Lerp(currentStrength, 0, cameraShakeReturnSpeed * Time.deltaTime);
        currentRotation = new Vector3(0, 0, Mathf.Sin(shakeTime * zShapeFaze) * currentStrength);

        cameraShakeParent.transform.localRotation = Quaternion.Euler(currentRotation);
        shakeTime += Time.deltaTime;
    }

    void Update()
    {
        HandleVerticalRecoil();
        HandleCameraShake();
    }
}
