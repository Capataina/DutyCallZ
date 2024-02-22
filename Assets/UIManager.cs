using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI purchaseText;

    private GameObject player;

    private void Update()
    {
        CheckStationDistance();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateScore(float score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateAmmo(float ammoInMagazine, float totalBullets)
    {
        ammoText.text = ammoInMagazine + "/" + totalBullets;
    }

    public void UpdatePurchaseText(string weaponOrBuffName, float weaponOrBuffCost)
    {
        purchaseText.text = ($"Do you want to buy {weaponOrBuffName} for {weaponOrBuffCost} points?");
    }

    public void UpdateReplenishText(float ammoCost)
    {
        purchaseText.text = ($"Do you want to replenish this weapons ammo for {ammoCost}?");
    }

    void CheckStationDistance()
    {
        var playerPosition = player.transform.position;
        var stationsLayerMask = LayerMask.GetMask("Weapon Stations", "Buff Stations");
        Collider[] allStations = Physics.OverlapSphere(playerPosition, 2f, stationsLayerMask);
        
        GameObject closestStation;

        if (allStations.Length == 0)
        {
            closestStation = null;
            purchaseText.gameObject.SetActive(false);
            return;
        }
        else
        {
            closestStation = allStations[0].gameObject;
        }

        foreach (var station in allStations)
        {
            if (Vector3.Distance(player.transform.position, station.gameObject.transform.position) <=
                Vector3.Distance(player.transform.position, closestStation.transform.position))
            {
                closestStation = station.gameObject;
            }
        }

        if (closestStation)
        {
            if (closestStation.gameObject.layer == LayerMask.NameToLayer("Weapon Stations"))
            {
                var weaponStation = closestStation.GetComponent<StationWeapon>();
                var playersWeapon = player.GetComponent<PlayerShooting>().heldWeapon;
                var playerWeaponPickup = player.GetComponent<WeaponPickup>();

                if (!playerWeaponPickup.inventory.Contains(weaponStation.weapon))
                {
                    UpdatePurchaseText(weaponStation.weapon.ToString(), weaponStation.weaponCost);
                }
                else if (playersWeapon.weaponType == weaponStation.weapon)
                {
                    UpdateReplenishText(weaponStation.ammoCost);
                }
            }
            else if (closestStation.gameObject.layer == LayerMask.NameToLayer("Buff Stations"))
            {
                var buffStation = closestStation.GetComponent<StationBuff>();

                UpdatePurchaseText(buffStation.buff.ToString(), buffStation.buffCost);
                

            }
            purchaseText.gameObject.SetActive(true);
        }
    }
}