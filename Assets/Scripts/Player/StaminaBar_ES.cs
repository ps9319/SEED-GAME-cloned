using UnityEngine;
using UnityEngine.UI;

public class StaminaBar_ES : MonoBehaviour
{
    [SerializeField] RectTransform StaminaBarfillRect;
    [SerializeField] float maxStamina = 100f;
    private float currentStamina;

    private float originalWidth;

    void Start()
    {
        currentStamina = maxStamina;
        originalWidth = StaminaBarfillRect.sizeDelta.x;
        UpdateStaminaBar();
    }

    void Update()
    {
        // �����̽��� ������ ���¹̳� 10 �Ҹ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseStamina(10f);
        }

        // ��Ŭ�� ������ ���¹̳� 5 ȸ��
        if (Input.GetMouseButtonDown(0))  // 0: ���� Ŭ��
        {
            RecoverStamina(5f);
        }
    }

    void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaBar();
    }

    void RecoverStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaBar();
    }

    void UpdateStaminaBar()
    {
        float staminaPercent = currentStamina / maxStamina;
        StaminaBarfillRect.sizeDelta = new Vector2(originalWidth * staminaPercent, StaminaBarfillRect.sizeDelta.y);
    }
}
