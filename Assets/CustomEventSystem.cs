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
}
