using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public List<GameObject> currentWeapons;
    public WeaponsClass heldWeapon;

    private WeaponTilt weaponTilt;
    private WeaponSway weaponSway;
    private CameraRecoilController playerCameraRecoilController;
    // private MeleeAttack meleeAttack;
    private Camera playerCamera;

    [Header("Melee")]
    [SerializeField] private float meleeDamage;
    [SerializeField] private LayerMask meleeMask;
    [SerializeField] private float meleeRange;
    [SerializeField] private float meleeRadius;

    [Header("Grenade")]
    [SerializeField] private GameObject greande;
    [SerializeField] private Vector3 throwForce;
    [SerializeField] private float torque;

    private void Awake()
    {
        weaponTilt = GetComponentInChildren<WeaponTilt>();
        weaponSway = GetComponentInChildren<WeaponSway>();
        playerCameraRecoilController = GetComponentInChildren<CameraRecoilController>();
        playerCamera = Camera.main;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }

        // TODO: THIS MIGHT NOT NEED TO BE IN HERE
        /// ------------------------
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapons.Count > 0)
        {
            ActivateWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapons.Count > 1)
        {
            ActivateWeapon(1);
        }
        /// ------------------------

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

    private void ThrowGrenade()
    {
        GameObject newGrenade = Instantiate(greande);
        newGrenade.transform.position = Camera.main.transform.position + transform.forward * 0.5f;
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.rotation * throwForce, ForceMode.Impulse);
        var x = Random.Range(-torque, torque);
        var y = Random.Range(-torque, torque);
        var z = Random.Range(-torque, torque);
        rb.AddTorque(new Vector3(x, y, z));
    }

    public void ActivateWeapon(int weaponIndex)
    {
        // activate weapon at a given index of inventory
        for (int i = 0; i < currentWeapons.Count; i++)
        {
            if (i == weaponIndex)
            {
                currentWeapons[i].SetActive(true);
                // print(currentWeapons[i].name);
                heldWeapon = currentWeapons[i].GetComponent<WeaponsClass>();
            }
            else
            {
                currentWeapons[i].SetActive(false);
            }
        }
        // assign every component the active weapon
        // TODO: RATHER THAN ASSIGNING EVERY COMOPNENT, EVERY COMPONENT THAT NEEDS THE WEAPON CAN READ IT
        var heldWeaponTransform = heldWeapon.transform;
        playerCameraRecoilController.currentWeapon = heldWeapon;
        weaponTilt.activeWeapon = heldWeaponTransform;
        weaponSway.weapon = heldWeaponTransform;
    }

    void MeleeAttack()
    {
        var playerCameraPosition = playerCamera.transform;
        if (Physics.SphereCast(playerCameraPosition.position, meleeRadius, playerCameraPosition.forward, out var objectHit, meleeRange, meleeMask))
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