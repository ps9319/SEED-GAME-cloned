using System;
using UnityEngine;

public class EnemyHealthUI: MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform healthBarFill; // Fill 이미지의 RectTransform
    [SerializeField] private GameObject healthBarUI;

    private float originalWidth; // 시작 너비 기억
    private float timeSinceLastHit = 0f;
    private bool isRecovering = false;
    private float recoverySpeed = 10f;
    private float hideDelay = 1f;
    private EnemyHealth enemyHealth;
    private float maxHealth;
    private float currentHealth;

    void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
        maxHealth = enemyHealth.getMaxHealth();
        
        if (healthBarFill != null)
            originalWidth = healthBarFill.sizeDelta.x;

        // 체력 안 보이게 하게 하는 코드
        // healthBarUI.SetActive(false);
    }

    void Update()
    {
        currentHealth = enemyHealth.getCurrentHealth();
        
        if (currentHealth < maxHealth)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit >= 5f && currentHealth > 0)
                isRecovering = true;
        }

        if (isRecovering)
        {
            RecoverHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        UpdateHealthBar();

        healthBarUI.SetActive(true);
        timeSinceLastHit = 0f;
        isRecovering = false;
        CancelInvoke(nameof(HideHealthBar));
    }

    void RecoverHealth()
    {
        currentHealth += recoverySpeed * Time.deltaTime;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            isRecovering = false;

            //체력 안 보이게 하는 코드 실행함
            // Invoke(nameof(HideHealthBar), hideDelay);
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float ratio = currentHealth / maxHealth;
        Vector2 newSize = healthBarFill.sizeDelta;
        newSize.x = originalWidth * ratio;
        healthBarFill.sizeDelta = newSize;
    }

    void HideHealthBar()
    {
        healthBarUI.SetActive(false);
    }
}