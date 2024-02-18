using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationEventListener : MonoBehaviour
{
    [SerializeField] BoxCollider hitbox;

    public void SpawnHitbox()
    {
        hitbox.enabled = true;
    }

    public void RemoveHitbox()
    {
        hitbox.enabled = false;
    }
}
