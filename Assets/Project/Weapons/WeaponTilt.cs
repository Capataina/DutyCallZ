using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTilt : MonoBehaviour
{
    [SerializeField] public Transform activeWeapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float strength;

    void Update()
    {
        // Tilt the weapon in response to quick mouse movements.
        if (activeWeapon)
        {
            float deltaX = Input.GetAxisRaw("Mouse X");
            float deltaY = Input.GetAxisRaw("Mouse Y");

            Quaternion newRotation = Quaternion.Euler(deltaY * strength * Time.deltaTime, deltaX * strength * Time.deltaTime, 0);

            activeWeapon.localRotation *= newRotation;

            if (activeWeapon.transform.localEulerAngles.sqrMagnitude > 0.05f)
            {
                activeWeapon.localRotation = Quaternion.Slerp(activeWeapon.localRotation, Quaternion.identity, returnSpeed);
            }
            else if (deltaX == 0 && deltaY == 0)
            {
                activeWeapon.localRotation = Quaternion.Slerp(activeWeapon.localRotation, Quaternion.identity, returnSpeed);
            }
        }

    }
}
