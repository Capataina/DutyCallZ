using Unity.VisualScripting;
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
    private Transform player;
    private float health;
    private bool isAttacking;


    private void Start()
    {
        //CustomEventSystem.current.onZombieTakeDamage += TakeDamageEvent;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        agent.destination = player.position;

        // setup hurtboxes
        Hurtbox[] hurtboxes = GetComponentsInChildren<Hurtbox>();
        foreach (Hurtbox hurtbox in hurtboxes)
        {
            hurtbox.takeDamageFunction = TakeDamageEvent;
        }
    }

    public void TakeDamageEvent(float damage)
    {
        //if (damage > armor)
        //{
        //    damage -= armor;
        //}
        //else
        //{
        //    damage = Mathf.Round(armor / damage);
        //}
        health -= damage;
        print("zombie took: " + damage + " damage");
        if (health <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        agent.destination = player.position;

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

    public void ObjectParameter(object test)
    {
        print("received: " + test);
    }

    public void AdjustHealthAndArmor(int waveCount)
    {
        float baseHealth = 80; // Starting health at wave 1
        float maxHealthIncrease = 40; // Total increase to reach 120 by wave 20

        // Calculate average health increase per wave to reach the target by wave 20
        float averageHealthPerWave = maxHealthIncrease / 19; // There are 19 intervals to increase over 20 waves

        // Calculate the current wave's average health, clamping to ensure it doesn't exceed the target maximum
        float currentWaveAverageHealth = Mathf.Clamp(baseHealth + (averageHealthPerWave * (waveCount - 1)), baseHealth, baseHealth + maxHealthIncrease);

        // Increase the max variance for health to provide a bigger spread of possible health values
        float healthVarianceRange = Mathf.Lerp(0, 25, (waveCount - 1) / 19f); // Increase max variance to +/-20 by the final wave
        float randomVariance = Random.Range(-healthVarianceRange, healthVarianceRange);

        // Calculate the actual health, ensuring it stays within the desired range
        maxHealth = Mathf.Round(Mathf.Clamp(currentWaveAverageHealth + randomVariance, baseHealth, 150));
        health = maxHealth;

        // For armor, implementing a similar approach for progressive and slightly randomized scaling
        float armorScale = Mathf.Lerp(0, 3, (waveCount - 1) / 19f);
        armor = Mathf.Round(Mathf.Clamp(armorScale + Random.Range(-1f, 1f), 0, 5)); // Slightly increase variance in armor as well

        Debug.Log($"Zombie health set to: {health}, armor set to: {armor}");
    }


}