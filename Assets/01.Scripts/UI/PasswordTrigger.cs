using UnityEngine;
using UnityEngine.UI;

public class PasswordTrigger : MonoBehaviour
{
    public GameObject passwordPanel; // 키패드 UI
    public GameObject interactionUI;     // "F 키를 눌러 상호작용하세요" 텍스트
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                passwordPanel.SetActive(true); // 키패드 열기
                Time.timeScale = 0f; // (선택사항) 게임 일시정지
            }
        }
        else
        {
            interactionUI.SetActive(false); // 멀어지면 메시지 끄기
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionUI.SetActive(true); // 👉 여기 추가!
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionUI.SetActive(false);
        }
    }
}