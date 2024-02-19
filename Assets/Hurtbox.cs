using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] float damageMultiplier;
    [SerializeField] Color damageColor = Color.white;
    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        float totalDamage = damage * damageMultiplier;
        CustomEventSystem.current.ZombieTakeDamage(totalDamage);
        CustomEventSystem.current.SpawnDamageText(totalDamage, hitPoint, damageColor);
    }
}
