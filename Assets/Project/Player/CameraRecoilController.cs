using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraRecoilController : MonoBehaviour
{

    [SerializeField] Transform cameraAnchor;
    [SerializeField] Transform playerCameraParent;
    [SerializeField] Transform cameraShakeParent;
    [SerializeField] public WeaponsClass currentWeapon;
    [SerializeField] float maxReturnHeight;
    [SerializeField] float cameraShakeReturnSpeed;
    [SerializeField] float cameraShakeSpeed;
    [SerializeField] float zShapeFaze;

    float time;
    float duration;
    float height;
    float shakeTime;
    float currentStrength;
    bool returning = false;
    bool recoilProcessing = false;

    Vector3 targetRotation;
    Vector3 currentRotation;
    [HideInInspector] public Quaternion recoilResetTarget;

    public void AddRecoil(float xRecoil, float duration)
    {
        // Vertical Recoil
        height = xRecoil;
        this.duration = duration;
        time = duration;
        recoilProcessing = true;
        returning = false;
    }

    public void AddCameraShake(float XShakeStrengh, float YShakeStrengh, float ZShakeStrength)
    {
        currentStrength = ZShakeStrength;
    }

    // Transform the angles from 90-0 and 360-270 to one continuous
    // 180-0 spectrum for comparisons
    private float TransformToLinearSpace(float angle)
    {
        if (angle > 270)
        {
            return angle - 270;
        }
        else
        {
            return angle + 90;
        }
    }

    void HandleVerticalRecoil()
    {
        if (time > 0)
        {
            float rotAmount = -height * Time.deltaTime / duration;
            cameraAnchor.Rotate(rotAmount, 0, 0, Space.Self);
            time -= Time.deltaTime;
            returning = false;
        }
        else if (recoilProcessing)
        {
            float camX = cameraAnchor.localRotation.eulerAngles.x;
            camX = TransformToLinearSpace(camX);
            float targetX = recoilResetTarget.eulerAngles.x;
            targetX = TransformToLinearSpace(targetX);
            bool lowerThanTarget = camX > targetX;
            if (Quaternion.Angle(cameraAnchor.localRotation, recoilResetTarget) > 0.15f && !lowerThanTarget)
            {
                returning = true;
                cameraAnchor.localRotation = Quaternion.RotateTowards(cameraAnchor.localRotation, recoilResetTarget, currentWeapon.recoilReturnSpeed * Time.deltaTime);
            }
            else
            {
                returning = false;
                recoilProcessing = false;
            }
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


    private void LateUpdate()
    {
        float deltaX = Input.GetAxisRaw("Mouse X");
        float deltaY = Input.GetAxisRaw("Mouse Y");
        if (deltaY > 0)
        {
            returning = false;
        }
        if (deltaX != 0 && deltaY != 0 && !returning)
        {
            recoilResetTarget = cameraAnchor.transform.localRotation;
        }
    }
}
