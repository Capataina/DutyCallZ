using System;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem current;

    private void Awake()
    {
        current = this;
    }

    // Zombie Events
    public event Action<float> onZombieTakeDamage;
    public void ZombieTakeDamage(float damage)
    {
        if (onZombieTakeDamage != null)
        {
            onZombieTakeDamage.Invoke(damage);
        }
    }

    public event Action<float, Vector3, Color> onSpawnDamageText;
    public void SpawnDamageText(float damage, Vector3 position, Color textColor)
    {
        if (onSpawnDamageText != null)
        {
            onSpawnDamageText.Invoke(damage, position, textColor);
        }
    }
}
