using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public WeaponType weapon;
    public float weaponCost;
    public float ammoCost;
    public enum WeaponType
    {
        Vector,
        KS23,
        Glock
    }
}
