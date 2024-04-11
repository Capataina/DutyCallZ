using System;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<float, Transform, Color> onSpawnDamageText;
    public void SpawnDamageText(float damage, Transform transform, Color textColor)
    {
        onSpawnDamageText?.Invoke(damage, transform, textColor);
    }
}
