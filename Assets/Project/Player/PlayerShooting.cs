using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public List<GameObject> currentWeapons;
    public WeaponsClass heldWeapon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapons.Count > 0)
        {
            print("pressed 1");
            ActivateWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapons.Count > 1)
        {
            ActivateWeapon(1);
        }

        if (heldWeapon)
        {
            if (heldWeapon.automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    heldWeapon.Fire();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    heldWeapon.Fire();
                }
            }
        }
    }

    public void ActivateWeapon(int weaponIndex)
    {
        for (int i = 0; i < currentWeapons.Count; i++)
        {
            if (i == weaponIndex)
            {
                currentWeapons[i].SetActive(true);
                print(currentWeapons[i].name);
                heldWeapon = currentWeapons[i].GetComponent<WeaponsClass>();
            }
            else
            {
                currentWeapons[i].SetActive(false);
            }
        }
    }
}