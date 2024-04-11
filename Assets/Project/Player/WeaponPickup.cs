using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    public List<StationWeapon.WeaponType> inventory = new List<StationWeapon.WeaponType>(); // Ensure inventory is initialized
    [SerializeField] private GameObject attachWeapon;
    [SerializeField] private GameObject weaponParent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GiveWeapon(GameObject weapon)
    {
        var playerCameraRecoilController = player.GetComponent<CameraRecoilController>();
        var playerShooting = player.GetComponent<PlayerShooting>();

        GameObject newWeapon = Instantiate(weapon, attachWeapon.transform, false);
        newWeapon.transform.localPosition = Vector3.zero;

        var newWeaponClass = newWeapon.GetComponent<WeaponsClass>();

        newWeaponClass.playerCamera = Camera.main;

        StationWeapon stationWeapon = newWeapon.GetComponent<StationWeapon>(); // Assuming this exists to get the weapon type

        if (playerShooting.currentWeapons.Count < 2)
        {
            // If less than 2 weapons, just add the new weapon
            playerShooting.currentWeapons.Add(newWeapon);
            playerShooting.ActivateWeapon(playerShooting.currentWeapons.Count - 1);
        }
        else
        {
            // If already 2 weapons, replace the currently held weapon
            int replaceIndex = playerShooting.currentWeapons.IndexOf(playerShooting.heldWeapon.gameObject);

            // If the held weapon is not found for some reason, default to replacing the first weapon
            if (replaceIndex == -1) replaceIndex = 0;

            var oldWeaponClass = playerShooting.currentWeapons[replaceIndex].GetComponent<WeaponsClass>();


            inventory.Remove(oldWeaponClass.weaponType); // Remove the weapon type from inventory


            // Remove the current held weapon from the game
            Destroy(playerShooting.currentWeapons[replaceIndex]);

            if (stationWeapon)
            {
                inventory.Add(stationWeapon.weapon); // Add the new weapon type
            }

            // Replace with the new weapon
            playerShooting.currentWeapons[replaceIndex] = newWeapon;
            playerShooting.ActivateWeapon(replaceIndex);
        }

        newWeaponClass.cameraController = player.GetComponent<CameraController>();
        newWeaponClass.recoilController = playerCameraRecoilController;

        newWeaponClass.weaponRecoilAnimation = weaponParent.GetComponent<WeaponRecoilAnimation>();
    }

}
