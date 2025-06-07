using UnityEngine;
using UnityEngine.UI;

public class HealthBar_ES : MonoBehaviour
{
    [SerializeField] RectTransform HealthBarfillRect;
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;

    private float originalWidth;
    private PlayerStun stun;

    void Start()
    {
        stun = GetComponent<PlayerStun>();
        currentHealth = maxHealth;
        originalWidth = HealthBarfillRect.sizeDelta.x;
        UpdateHealthBar();
    }

    void Update()
    {
        
    }

    public void OnHitboxTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon == null) return;

        float damage = weapon.Damage;
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        stun.ApplyStun(3f);
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        HealthBarfillRect.sizeDelta = new Vector2(originalWidth * healthPercent, HealthBarfillRect.sizeDelta.y);
    }
}
