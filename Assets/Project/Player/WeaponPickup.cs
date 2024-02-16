using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    private float holdDownTimer;
    private List<StationWeapon.WeaponType> inventory = new List<StationWeapon.WeaponType>(); // Ensure inventory is initialized

    private GameObject[] allWeaponStations; // Array to hold all weapon stations
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Find all game objects with the tag "Weapon Station" and assign them to the array
        allWeaponStations = GameObject.FindGameObjectsWithTag("Weapon Station");
    } 

    // Update is called once per frame
    void Update()
    {
        CheckDistance(allWeaponStations); // Now passing an array of all weapon stations
    }

    void GiveWeapon(GameObject weapon)
    {
        // Debug.Log("Spawned weapon");
        var weaponParent = GameObject.FindGameObjectWithTag("WeaponParent");
        var playerCameraRecoilController = player.GetComponent<CameraRecoilController>();
        
        GameObject newWeapon = Instantiate(weapon, weaponParent.transform, false);
        newWeapon.transform.localPosition = Vector3.zero;
        
        var newWeaponClass = newWeapon.GetComponent<WeaponsClass>();
        
        newWeaponClass.playerCamera = Camera.main;
        player.GetComponent<PlayerShooting>().gun = newWeaponClass;
        playerCameraRecoilController.currentWeapon = newWeaponClass;
        
        newWeaponClass.cameraController = player.GetComponent<CameraController>();
        newWeaponClass.recoilController = playerCameraRecoilController;
        
        weaponParent.GetComponent<WeaponSway>().activeWeapon = newWeapon.transform;
        weaponParent.GetComponent<WeaponRock>().weapon = newWeapon.transform;
    }
    
    private void CheckDistance(GameObject[] weaponStations)
    {
        float closestDistance = Mathf.Infinity; // Track the closest distance found
        GameObject closestStation = null; // Track the closest station

        // Iterate over all weapon stations to find the closest one
        foreach (var station in weaponStations)
        {
            float distance = Vector3.Distance(player.transform.position, station.transform.position);
            if (distance < closestDistance)
            {
                Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Weapon Stations"));
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject == station)
                    {
                        closestDistance = distance;
                        closestStation = station;
                        break; // Found a station within range, no need to check further
                    }
                }
            }
        }

        // If a closest station is found and within range, process the weapon pickup
        if (closestStation != null && closestDistance <= 5f)
        {
            // Debug.Log("In distance to the closest station");
            if (Input.GetKey(KeyCode.E))
            {
                // Debug.Log("Holding E");
                holdDownTimer += Time.deltaTime;

                if (holdDownTimer >= 1)
                {
                    // Debug.Log("Giving weapon");
                    StationWeapon stationWeapon = closestStation.GetComponent<StationWeapon>();
                    
                    if (!inventory.Contains(stationWeapon.weapon))
                    {
                        inventory.Add(stationWeapon.weapon);
                        GiveWeapon(stationWeapon.weaponPrefab);
                        holdDownTimer = 0;
                    }
                }
            }
            else
            {
                holdDownTimer = 0;
            }
        }
    }
}
