using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public WeaponType weapon;
    public enum WeaponType
    {
        Vector,
        KS23
    }
}
