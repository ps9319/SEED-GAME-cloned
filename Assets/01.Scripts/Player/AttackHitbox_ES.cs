using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    
    [SerializeField] private float stunDuration = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealthUI enemyHealth = other.GetComponentInChildren<EnemyHealthUI>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20f);
            }
        }

        // 스턴
        var stun = other.GetComponent<EnemyStun>();
        if (stun != null)
        {
            stun.ApplyStun(stunDuration);
        }
    }
}

