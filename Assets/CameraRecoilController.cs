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
    float perlinTime;

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
        //float zShake = Random.Range(-ZShakeStrength, ZShakeStrength);
        float zShake = Mathf.PerlinNoise1D(perlinTime) * ZShakeStrength * 2 - ZShakeStrength;
        //float zShake = Mathf.Sin(perlinTime) * ZShakeStrength;
        float yShake = Random.Range(-YShakeStrengh, YShakeStrengh);

        targetRotation += new Vector3(0, yShake, zShake);
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
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, cameraShakeReturnSpeed * Time.deltaTime);
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, cameraShakeSpeed * Time.deltaTime);

        cameraShakeParent.transform.localRotation = Quaternion.Euler(currentRotation);
        perlinTime += Time.deltaTime * zShapeFaze;
    }

    void Update()
    {
        HandleVerticalRecoil();
        HandleCameraShake();
    }
}
