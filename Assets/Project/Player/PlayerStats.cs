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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthRegen = baseHealthRegen;
        currentArmor = baseArmor;
    }

    // Update is called once per frame
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

    public void takeDamage(float damage)
    {
        print("took: " + damage + " damage");
        cameraAnimator.Play("PlayerHit", 0);
    }
}
