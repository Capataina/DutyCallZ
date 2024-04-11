using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StationWeapon : Interactable
{
    public GameObject weaponPrefab;
    public WeaponType weapon;
    public float weaponCost;
    public float ammoCost;
    WeaponPickup weaponPickup;

    public enum WeaponType
    {
        Vector,
        KS23,
        Pistol1911,
        AK47,
    }

    private void Start()
    {
        weaponPickup = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponPickup>();
    }

    public override void Interact(GameObject other)
    {
        if (PlayerStats.current.currentScore >= weaponCost)
        {
            PlayerStats.current.currentScore -= weaponCost;
            weaponPickup.GiveWeapon(weaponPrefab);
        }
    }

    public override void DisplayPrompt()
    {
        UIManager.instance.DisplayText($"Do you want to buy {weapon} for {weaponCost} points?");
    }
}
