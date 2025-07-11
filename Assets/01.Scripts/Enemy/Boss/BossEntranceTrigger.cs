using UnityEngine;

public class BossEntranceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject bossCam; 
    [SerializeField] private AudioSource entranceSound;

    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject playerObject;
    private PlayerMovement playerMovement;

    private bool hasTriggered = false;
    private GameObject playerCam;

    private void Start()
    {
        // 예: 플레이어 오브젝트에서 PlayerMovement 컴포넌트를 찾아서 저장
        if (playerObject != null)
            playerMovement = playerObject.GetComponent<PlayerMovement>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            bossObject.SetActive(true);
            bossAnimator.SetTrigger("Enter");
            entranceSound.Play();

            // UI 끄기
            if (playerUI != null)
                playerUI.SetActive(false);

            // 플레이어 움직임 끄기
            if (playerMovement != null)
                playerMovement.enabled = false;

            // 카메라 전환
            bossCam.SetActive(true);

            StartCoroutine(ReturnControlAfterDelay(3.0f));
        }
    }

    private System.Collections.IEnumerator ReturnControlAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // UI 켜기
        if (playerUI != null)
            playerUI.SetActive(true);

        // 플레이어 움직임 켜기
        if (playerMovement != null)
            playerMovement.enabled = true;

        // 카메라 복귀
        bossCam.SetActive(false);
    }
}
