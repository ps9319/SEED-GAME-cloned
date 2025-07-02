using UnityEngine;

public class HealthBar_ES : MonoBehaviour
{
    [SerializeField] RectTransform healthBarfillRect;
    
    private float maxHealth;
    private float currentHealth;
    private float originalWidth;

    private void Awake()
    {
        originalWidth = healthBarfillRect.sizeDelta.x;
    }

    public void Init(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        healthBarfillRect.sizeDelta = new Vector2(originalWidth * healthPercent, healthBarfillRect.sizeDelta.y);
    }
}
