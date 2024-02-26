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
        PlayerStats.current.currentScore += Mathf.Floor(totalDamage / 2);
        // UIManager.instance.UpdateScore(PlayerStats.current.currentScore);
        takeDamageFunction.Invoke(totalDamage);
        CustomEventSystem.current.SpawnDamageText(Mathf.RoundToInt(totalDamage), hitPoint, damageColor);
    }
}
