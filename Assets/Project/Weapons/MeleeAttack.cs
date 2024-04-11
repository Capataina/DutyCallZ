using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Not sure where this is used

public class MeleeAttack : MonoBehaviour
{
    public BoxCollider meleeCollider;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        meleeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeCollider.enabled)
        {
            StartCoroutine(DisableMeleeCollider());
        }
    }

    private IEnumerator DisableMeleeCollider()
    {
        yield return new WaitForSeconds(1f);
        meleeCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("enter collision");
        if (other.CompareTag("Zombie"))
        {
            Zombie zombieStats = other.GetComponent<Zombie>();
            zombieStats.TakeDamageEvent(damage);
        }
    }
}
