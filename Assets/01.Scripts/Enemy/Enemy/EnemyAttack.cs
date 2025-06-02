using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public GameObject attackHitbox;
    public Animator animator;

    private Transform player;
    private float lastAttackTime = Mathf.NegativeInfinity;
    private EnemyStun stun;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Player는 "Player" 태그 있어야 함
        stun = GetComponent<EnemyStun>(); // 스턴 상태 가져오기
        attackHitbox.SetActive(false);
    }

    void Update()
    {
        

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }

    }

    void Attack()
    {
        // 스턴 중이면 공격 금지
        if (stun != null && stun.IsStunned()) return;
        
        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // 히트박스는 애니메이션 이벤트로 EnableAttackHitbox / DisableAttackHitbox를 호출하는 것이 좋음
        Invoke(nameof(EnableAttackHitbox), 0.3f); // 타격 타이밍 맞추기
        Invoke(nameof(DisableAttackHitbox), 0.6f); // 끝나고 비활성화
    }

    void EnableAttackHitbox()
    {
        attackHitbox.SetActive(true);
    }

    void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }
}
