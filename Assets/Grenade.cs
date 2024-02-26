using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float startingDamage;
    [SerializeField] float range;
    [SerializeField] float explosionTime;
    [SerializeField] AnimationCurve falloff;
    [SerializeField] LayerMask hitMask;

    private void Start()
    {
        StartCoroutine(Explode());
    }


    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);
        print("ran");

        Collider[] hits = Physics.OverlapSphere(transform.position, range, hitMask);
        foreach (Collider hit in hits)
        {
            Hurtbox hurtbox = hit.GetComponent<Hurtbox>();
            float dist = Vector3.Distance(hit.transform.position, transform.position);
            float damage = falloff.Evaluate(dist / range) * startingDamage;
            hurtbox.TakeDamage(damage, hit.transform.position);
        }

        Destroy(gameObject);
    }
}
