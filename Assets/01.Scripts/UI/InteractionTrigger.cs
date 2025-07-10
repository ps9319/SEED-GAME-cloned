using System.Collections;
using System.Net.Mime;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject interactionUI; // "F키를 눌러 상호작용" 텍스트
    public TextManager TextManager;
    public KeyCode interactionKey = KeyCode.F;
    
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            interactionUI.SetActive(false);
            TextManager.ShowClueMessage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            StartCoroutine(DestroyAfterTime());
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

    private IEnumerator DestroyAfterTime()
    {
        float totalDisplayTime = TextManager.displayTime + TextManager.fadeDuration;
        yield return new WaitForSeconds(totalDisplayTime);
        Destroy(gameObject);
    }
}

