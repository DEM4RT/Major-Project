using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    
    private void Start()
    {
        Invoke(nameof(Impact), 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            return;
        }

        if (other.TryGetComponent(out ZombieHealth enemy))
        {
            enemy.TakeDamage(damage);
        }

        Impact();
    }

    private void Impact()
    {
        Destroy(gameObject);
    }
}
