using System;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<float, Vector3, Color> onSpawnDamageText;
    public void SpawnDamageText(float damage, Vector3 position, Color textColor)
    {
        onSpawnDamageText?.Invoke(damage, position, textColor);
    }
}
