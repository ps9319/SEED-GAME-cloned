using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private PlayerStun stun;
    
    private void Awake()
    {
        stun = GetComponent<PlayerStun>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponentInParent<Weapon>();
        if (weapon == null) return;

        float damage = weapon.Damage;
        TakeDamage(damage);
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log("플레이어 피격 데미지: " + damage);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        stun.ApplyStun(3f);
        HealthBar_ES playerHealthUI = GetComponentInChildren<HealthBar_ES>();
        playerHealthUI.TakeDamage(damage);
    }

    private void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
    
    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
