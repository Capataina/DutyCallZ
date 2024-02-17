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
        if (damage > armor)
        {
            damage -= armor;
        }
        else
        {
           damage = Mathf.Round(armor / damage);
        }
        
        health -= damage;
        // Debug.Log(health);
        
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
    }
    
    private void Die()
    {
        Destroy(gameObject); // Destroy the entire zombie game object
    }
    
    // public void AdjustHealthAndArmor(int waveCount)
    // {
    //     // Calculate the progress towards the max wave for scaling
    //     float progress = Mathf.Clamp01((waveCount - 1) / 19f); // Scales from wave 1 to 20
    //
    //     // Adjust randomness factor with a more controlled growth
    //     float healthRandomFactor = Mathf.Pow(progress, 2); // Squaring the progress to slow down scaling at start
    //     float healthRandomness = Random.Range(0f, 1f) * (0.5f + 0.5f * healthRandomFactor); // Ensuring it starts from 0 and gradually increases
    //
    //     // For armor, we use a simpler approach since its max value is much lower
    //     float armorRandomness = Random.Range(0f, 1f) * (0.5f + 0.5f * Mathf.Sqrt(progress)); // Using square root to slow growth
    //
    //     // Use the randomness factor to skew towards higher values more in later waves
    //     float healthScale = Mathf.Lerp(80, 120, Mathf.Clamp01(healthRandomness));
    //     float armorScale = Mathf.Lerp(0, 3, Mathf.Clamp01(armorRandomness));
    //
    //     maxHealth = healthScale;
    //     health = maxHealth;
    //     armor = armorScale;
    //
    //     Debug.Log($"Zombie health adjusted to: {health}, armor adjusted to: {armor}");
    // }
    
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