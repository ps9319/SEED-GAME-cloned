using UnityEngine;
using UnityEngine.UI;

public class HealthBar_ES : MonoBehaviour
{
    [SerializeField] RectTransform HealthBarfillRect;
    [SerializeField] float maxHealth = 100f;
    private float currentHealth;

    private float originalWidth;

    void Start()
    {
        currentHealth = maxHealth;
        originalWidth = HealthBarfillRect.sizeDelta.x;
        UpdateHealthBar();
    }

    void Update()
    {
        // �����̽��� ������ ������ 10
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10f);
        }

        // ��Ŭ�� ������ ü�� ȸ�� 5
        if (Input.GetMouseButtonDown(0))  // 0: ���� Ŭ��
        {
            Heal(5f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
