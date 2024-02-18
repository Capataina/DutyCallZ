using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] float damageMultiplier;
    public void TakeDamage(float damage)
    {
        CustomEventSystem.current.ZombieTakeDamage(damageMultiplier * damage);
    }
}
