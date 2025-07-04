using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    SkillAttack1,
    SkillAttack2,
    Hit,
    Dead
}

public class EnemyAI : MonoBehaviour
{
    private EnemyState currentState = EnemyState.Idle;

    private EnemyInfos enemyInfos;
    private Transform player;
    private EnemyMovement movement;
    private Animator animator;
    private EnemyStun stun;
    private BossSkill bossSkill;


    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        enemyInfos = GetComponent<Enemy>().enemyInfos;
        stun = GetComponent<EnemyStun>();
        bossSkill = GetComponent<BossSkill>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator.SetTrigger("Idle");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        HandleState(distance);
    }

    private void HandleState(float distance)
    {
        EnemyState newState = DetermineState(distance);

        // SkillAttack 상태일 때 → 진행 중이면 상태 고정
        if (currentState == EnemyState.SkillAttack1 || currentState == EnemyState.SkillAttack2)
        {
            if (bossSkill != null && bossSkill.IsSkillInProgress())
                return;

            animator.ResetTrigger(currentState.ToString());
            animator.SetTrigger(newState.ToString());
            currentState = newState;
            return;
        }

        


        if (player == null) return;

        if (stun != null && stun.isStunned)
        {
            if (currentState != EnemyState.Hit && currentState != EnemyState.Dead)
            {
                animator.ResetTrigger(currentState.ToString());
                animator.SetTrigger(EnemyState.Hit.ToString());
                currentState = EnemyState.Hit;

                movement.Stop();
            }
            return; // 스턴 중에는 상태머신 정지
        }

        // Chase일 때는 계속해서 Move를 호출해야함
        if (currentState == EnemyState.Chase)
            movement.Move(player.position);
        if (currentState == newState && currentState != EnemyState.Dead) return;

        animator.ResetTrigger(currentState.ToString());
        animator.SetTrigger(newState.ToString());
        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Idle:
            case EnemyState.Attack:
            case EnemyState.Hit:
            case EnemyState.Dead:
                movement.Stop();
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.SkillAttack1:
                movement.Stop();
                bossSkill?.TryCastSkill1();
                break;
            case EnemyState.SkillAttack2:
                movement.Stop();
                bossSkill?.TryCastSkill2();
                break;
        }
    }

    private EnemyState DetermineState(float distance)
    {
        if (currentState == EnemyState.Dead) return EnemyState.Dead;
        if (distance < enemyInfos.attackInfo.attackRange)
        {
            // 보스 공격
            if (bossSkill != null && bossSkill.CanUseSkill())
            {
                // 예: 랜덤으로 Skill1 또는 Skill2 중 하나 선택
                if (Random.value < 0.5f)
                    return EnemyState.SkillAttack1;
                else
                    return EnemyState.SkillAttack2;
            }
            // 일반 공격
            return EnemyState.Attack;
        }
        if (distance < enemyInfos.detectionRange) return EnemyState.Chase;
        if (distance < enemyInfos.detectionRange)
            return EnemyState.Chase;

        return EnemyState.Idle;
    }

    public void Die()
    {
        currentState = EnemyState.Dead;
    }
}