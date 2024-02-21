using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public List<GameObject> currentWeapons;
    public WeaponsClass heldWeapon;

    private WeaponSway weaponSway;
    private WeaponRock weaponRock;
    private CameraRecoilController playerCameraRecoilController;
    // private MeleeAttack meleeAttack;
    private Camera playerCamera;
    
    [Header("Melee")]
    [SerializeField] private float meleeDamage;
    [SerializeField] private LayerMask meleeMask;
    [SerializeField] private float meleeRange;
    [SerializeField] private float meleeRadius;
    private void Awake()
    {
        weaponSway = GetComponentInChildren<WeaponSway>();
        weaponRock = GetComponentInChildren<WeaponRock>();
        playerCameraRecoilController = GetComponentInChildren<CameraRecoilController>();
        // meleeAttack = GetComponentInChildren<MeleeAttack>();
        // meleeAttack.damage = playerMeleeDamage;
        playerCamera = Camera.main;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            MeleeAttack();
        }
        
        
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
        
        
        var heldWeaponTransform = heldWeapon.transform;
        playerCameraRecoilController.currentWeapon = heldWeapon;
        weaponSway.activeWeapon = heldWeaponTransform;
        weaponRock.weapon = heldWeaponTransform;

    }

    void MeleeAttack()
    {
        var playerCameraPosition = playerCamera.transform;
        if (Physics.SphereCast(playerCameraPosition.position,meleeRadius, playerCameraPosition.forward, out var objectHit, meleeRange, meleeMask))
        {
            Debug.DrawRay(playerCameraPosition.position, playerCameraPosition.position + playerCameraPosition.forward * meleeRange, Color.blue, 5);
            if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Zombies"))
            {
                var hurtbox = objectHit.collider.GetComponent<Hurtbox>();
                hurtbox.TakeDamage(meleeDamage, objectHit.point);
            }
        }
    }
}