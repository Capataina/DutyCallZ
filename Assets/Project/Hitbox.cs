using Unity.Profiling.LowLevel;
using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] BoxCollider hitbox;
    [SerializeField] float damage;


    private void Start()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats pStats = other.GetComponent<PlayerStats>();
            pStats.TakeDamage(damage);
        }
    }
}
