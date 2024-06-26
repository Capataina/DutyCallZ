using UnityEngine;

// Singleton component that holds all player relevant data

public class PlayerStats : MonoBehaviour
{
    public float currentScore = 0;

    private float currentHealth;
    [SerializeField] private float maxHealth;
    // TODO: Should probably not be here
    [SerializeField] Animator cameraAnimator;
    [HideInInspector] public float healthRegen;
    [SerializeField] public float baseHealthRegen;

    private float currentArmor;
    [SerializeField] private float baseArmor;

    public static PlayerStats current;

    private void Awake()
    {
        current = this;
    }


    void Start()
    {
        currentHealth = maxHealth;
        healthRegen = baseHealthRegen;
        currentArmor = baseArmor;
    }

    void Update()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegen * Time.deltaTime;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        damage -= currentArmor;
        cameraAnimator.Play("PlayerHit", 0);
    }
}
