using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    [SerializeField] public float damage = 20f;
    [SerializeField] public float stunDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // 체력 깎기
        if (other.CompareTag("Player"))
        {
            HealthBar_ES playerHealth = other.GetComponentInChildren<HealthBar_ES>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
            }
        }

        // 스턴 적용
        PlayerStun playerStun = other.GetComponent<PlayerStun>();
        if (playerStun != null)
        {
            playerStun.ApplyStun(stunDuration);
        }
    }
}

