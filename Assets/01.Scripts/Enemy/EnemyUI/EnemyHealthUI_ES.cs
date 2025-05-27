using UnityEngine;

public class EnemyHealth_Rect : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private RectTransform healthBarFill; // Fill �̹����� RectTransform
    [SerializeField] private GameObject healthBarUI;

    private float originalWidth; // ���� �ʺ� ���
    private float timeSinceLastHit = 0f;
    private bool isRecovering = false;
    private float recoverySpeed = 10f;
    private float hideDelay = 1f;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarFill != null)
            originalWidth = healthBarFill.sizeDelta.x;

        healthBarUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(20f);
        }

        if (currentHealth < maxHealth)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit >= 5f)
                isRecovering = true;
        }

        if (isRecovering)
        {
            RecoverHealth();
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f);

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
            Invoke(nameof(HideHealthBar), hideDelay);
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
