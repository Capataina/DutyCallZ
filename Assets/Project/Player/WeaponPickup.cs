using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private GameObject player;
    private float holdDownTimer;
    private List<StationWeapon.WeaponType> inventory;

    private GameObject vectorStation;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Weapon Stations")).Length);
        CheckDistance(vectorStation);
    }

    void GiveWeapon(GameObject weapon)
    {
        Debug.Log("spawned weapon");
        Instantiate(weapon);
        weapon.transform.parent = GameObject.FindGameObjectWithTag("WeaponParent").transform;
    }
    
    private void CheckDistance(GameObject weaponStation)
    {
        if (Vector3.Distance(player.transform.position, weaponStation.transform.position) <= 5)
        {
            Debug.Log("in distance");
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("holding e");
                holdDownTimer += Time.deltaTime;
                
                if (holdDownTimer >= 1)
                {
                    Debug.Log("giving weapon");
                    StationWeapon stationWeapon = weaponStation.GetComponent<StationWeapon>();
                    
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
