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
    private float storedPlayerScore = 0;
    private float storedPlayerAmmo = 0;
    private float storedPlayerBulletsInMag = 0;

    private GameObject player;

    private void Update()
    {
        var playersWeapon = player.GetComponent<PlayerShooting>().heldWeapon;

        if (Math.Abs(storedPlayerScore - PlayerStats.current.currentScore) > 0)
        {
            // print("scoreChanged");
            storedPlayerScore = PlayerStats.current.currentScore;
            UpdateScore(storedPlayerScore);
        }

        if (playersWeapon)
        {
            if (Math.Abs(storedPlayerAmmo - playersWeapon.currentAmmo) > 0 || Math.Abs(storedPlayerBulletsInMag - playersWeapon.bulletsInMag) > 0)
            {
                // print("ammo changed");
                storedPlayerBulletsInMag = playersWeapon.bulletsInMag;
                storedPlayerAmmo = playersWeapon.currentAmmo;
                UpdateAmmo(storedPlayerBulletsInMag, storedPlayerAmmo);
            }
        }

        //CheckStationDistance();
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

    public void DisplayText(string text)
    {
        purchaseText.text = text;
    }

    public void ClearText()
    {
        purchaseText.text = "";
    }

}