using UnityEngine;

public class HealthBar_ES : MonoBehaviour
{
    [SerializeField] RectTransform healthBarfillRect;
    
    private float maxHealth;
    private float currentHealth;
    private float originalWidth;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        maxHealth = playerHealth.getMaxHealth();
        currentHealth = maxHealth;
        originalWidth = healthBarfillRect.sizeDelta.x;
        UpdateHealthBar();
    }

    void Update()
    {
        currentHealth = playerHealth.getCurrentHealth();
    }

    // public void OnHitboxTriggerEnter(Collider other)
    // {
    //     Weapon weapon = other.GetComponent<Weapon>();
    //     if (weapon == null) return;
    //
    //     float damage = weapon.Damage;
    //     TakeDamage(damage);
    // }

    public void TakeDamage(float damage)
    {
        currentHealth = playerHealth.getCurrentHealth();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        healthBarfillRect.sizeDelta = new Vector2(originalWidth * healthPercent, healthBarfillRect.sizeDelta.y);
    }
}
