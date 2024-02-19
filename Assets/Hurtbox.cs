using System;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] float damageMultiplier;
    [SerializeField] Color damageColor = Color.white;
    [HideInInspector] public Action<float> takeDamageFunction;

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        float totalDamage = damage * damageMultiplier;
        takeDamageFunction.Invoke(totalDamage);
        CustomEventSystem.current.SpawnDamageText(totalDamage, hitPoint, damageColor);
    }
}
