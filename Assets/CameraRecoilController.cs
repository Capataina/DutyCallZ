using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraRecoilController : MonoBehaviour
{

    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerCameraParent;
    [SerializeField] WeaponsClass currentWeapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float returnForceDamp;
    [SerializeField] float maxReturnHeight;

    float time;
    float duration;
    float height;
    float currentReturnForce;
    bool recoilActive;

    public void AddRecoil(float xRecoil, float duration)
    {
        height = xRecoil;
        this.duration = duration;
        time = duration;
        recoilActive = true;
    }

    void Update()
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
}
