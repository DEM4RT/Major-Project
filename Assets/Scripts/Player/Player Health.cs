using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Player health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }
}
