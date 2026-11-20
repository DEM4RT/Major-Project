using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    private StatsManager stats;

    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Healing")]
    private InputAction healAction;
    public float healingTime = 5;
    private bool isHealing = false;
    private float healEndTime;
    
    private void Awake()
    {
        // Find the stat manager gameobject
        stats = FindAnyObjectByType<StatsManager>();

        // Set the current health to be equivalent to max health
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // Get the heal action from the input system
        healAction = InputSystem.actions.FindAction("Heal");
        healAction.started += OnHealButtonPressed;
    }
    
    private void Update()
    {
        // Check if the player is already healing and if the healing time has passed
        if (isHealing && Time.time >= healEndTime)
        {
            // Replenish the health
            currentHealth = maxHealth;
            isHealing = false;
        }
    }

    public void TakeDamage(float damage)
    {
        // Decrease the current health by amount of damage dealt
        currentHealth -= damage;

        // Call damage taken method
        stats.DamageTaken(damage);
        // Call zombie hit method
        stats.ZombieHits();

        Debug.Log("Player health: " + currentHealth);

        // If the current health is 0, player is dead
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }

    public void OnHealButtonPressed(InputAction.CallbackContext context)
    {
        Heal();
    }

    public void Heal()
    {
        // Check if already healing or if health is full
        if (isHealing || currentHealth == maxHealth)
        {
            return;
        }

        stats.HealingUsage();

        isHealing = true;

        healEndTime = Time.time + healingTime;
    }
}
