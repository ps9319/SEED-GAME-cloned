using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyInfos infos;
    
    private float maxHealth => infos.maxHealth;
    private float currentHealth { get; set; }
    private bool isDead { get; set; } = false;
    
    public event Action onDeath;
    

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon == null) return;
        float attackDamage = weapon.Damage;
        TakeDamage(attackDamage);
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath();
        isDead = true;
    }
}
