using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Zombie is dead!");
        Destroy(gameObject);
    }
}
