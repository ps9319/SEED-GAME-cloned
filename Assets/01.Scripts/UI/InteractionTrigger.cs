using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject interactionUI; // "F키를 눌러 상호작용" 텍스트
    public TextManager TextManager;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            interactionUI.SetActive(false);
            TextManager.ShowClueMessage();
        }
    }
}

