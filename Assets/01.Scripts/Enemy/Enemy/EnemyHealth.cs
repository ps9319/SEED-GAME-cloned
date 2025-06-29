using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyInfos infos;
    private float maxHealth => infos.maxHealth;
    private float currentHealth { get; set; }
    private bool isDead { get; set; } = false;
    private EnemyStun stun;
    private AudioSource audioSource;
    public event Action onDeath;

    private void Awake()
    {
        infos = GetComponent<Enemy>().enemyInfos;
        stun = GetComponent<EnemyStun>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnHitboxTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon == null) return;
        float attackDamage = weapon.Damage;
        TakeDamage(attackDamage);
        //Todo EnemyHealth, EnemyHealthUI와 통합 필요
        EnemyHealthUI enemyHealthUI = GetComponentInChildren<EnemyHealthUI>();
        enemyHealthUI.TakeDamage(attackDamage);
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        stun.ApplyStun(1f);
        // 피격 효과음 재생
        audioSource.PlayOneShot(infos.hitSound);
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath?.Invoke();
        isDead = true;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
    
}
