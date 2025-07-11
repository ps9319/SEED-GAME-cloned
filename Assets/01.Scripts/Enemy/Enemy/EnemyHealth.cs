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
    private EnemyAI enemyAI;
    public event Action onDeath;
    private bool puzzleTriggered = false;
    private BossPuzzleManager puzzleManager;

    private void Awake()
    {
        infos = GetComponent<Enemy>().enemyInfos;
        stun = GetComponent<EnemyStun>();
        audioSource = GetComponent<AudioSource>();
        enemyAI = GetComponent<EnemyAI>();
        puzzleManager = GetComponent<BossPuzzleManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnHitboxTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon == null) return;
        if (weapon.tag != "PlayerWeapon") return;

        float attackDamage = weapon.Damage;
        Debug.Log("attackDamage: " + attackDamage);
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

        // 퍼즐 조건
        if (!puzzleTriggered && currentHealth <= maxHealth * 0.2f)
        {
            puzzleTriggered = true;
            puzzleManager.StartPuzzle();
        }
        
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath?.Invoke();
        isDead = true;
        // Todo enemyAI가 onDeath 구독하게 바꾸기
        enemyAI.Die();
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
