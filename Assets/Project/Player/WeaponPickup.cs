using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    private float holdDownTimer;
    private List<StationWeapon.WeaponType> inventory = new List<StationWeapon.WeaponType>(); // Ensure inventory is initialized
    [SerializeField] private GameObject attachWeapon;
    [SerializeField] private GameObject weaponParent;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        CheckDistance(); // Searching through all weapon stations
        
    }

    void GiveWeapon(GameObject weapon)
    {
        // Debug.Log("Spawned weapon");
        var playerCameraRecoilController = player.GetComponent<CameraRecoilController>();
        
        GameObject newWeapon = Instantiate(weapon, attachWeapon.transform, false);
        newWeapon.transform.localPosition = Vector3.zero;
        
        var newWeaponClass = newWeapon.GetComponent<WeaponsClass>();
        
        newWeaponClass.playerCamera = Camera.main;
        player.GetComponent<PlayerShooting>().gun = newWeaponClass;
        playerCameraRecoilController.currentWeapon = newWeaponClass;
        
        newWeaponClass.cameraController = player.GetComponent<CameraController>();
        newWeaponClass.recoilController = playerCameraRecoilController;
        
        weaponParent.GetComponent<WeaponSway>().activeWeapon = newWeapon.transform;
        weaponParent.GetComponent<WeaponRock>().weapon = newWeapon.transform;

        newWeaponClass.weaponRecoilAnimation = weaponParent.GetComponent<WeaponRecoilAnimation>();
    }
    
    private void CheckDistance()
    {
        
        Collider[] weaponStations = Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Weapon Stations"));
        GameObject closestStation = null;

        if (weaponStations.Length > 0)
        {
            closestStation = weaponStations[0].gameObject;
        }

        foreach (var station in weaponStations)
        {
            if (Vector3.Distance(player.transform.position, station.gameObject.transform.position) <=
                Vector3.Distance(player.transform.position, closestStation.transform.position))
            {
                closestStation = station.gameObject;
            }
        }

        // If a closest station is found and within range, process the weapon pickup
        if (closestStation)
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
