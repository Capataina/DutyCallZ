using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange;
    [SerializeField] private float armor;
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider hitBox;
    private Transform goal;
    private float health;
    private bool isAttacking;
    public Transform player;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = goal.position;

        if (isAttacking)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        if (!isAttacking)
        {
            AnimationTools.CrossFadeToAnimationFT(animator, "Armature|Run", 0.15f, 0);
        }

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            AnimationTools.CrossFadeToAnimationFT(animator, "Armature|attack", 0.1f, 0);
            isAttacking = true;
        }

        if (isAttacking)
        {
            if (AnimationTools.AnimationIsActiveAndFinished(animator, "Armature|attack", 0))
            {
                isAttacking = false;
            }
        }
    }


    private void Die()
    {
        Destroy(gameObject); // Destroy the entire zombie game object
    }
}