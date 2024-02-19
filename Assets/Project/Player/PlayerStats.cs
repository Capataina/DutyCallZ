using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth;
    // Should probably not be here
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
        print("took: " + damage + " damage");
        cameraAnimator.Play("PlayerHit", 0);
    }
}
