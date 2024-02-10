using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float armor;
    private Transform goal;
    private float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        
        if (health < 0)
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
    }
    
    private void Die()
    {
        Destroy(gameObject); // Destroy the entire zombie game object
    }
}