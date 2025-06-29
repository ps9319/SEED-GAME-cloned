using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class BossSkill1 : MonoBehaviour
{
    public GameObject lightningEffectPrefab; // VFX Graph LightningPrefab
    public float attackDelay = 2.0f; // 시전 시간
    public float attackRadius = 5.0f; // 공격 범위
    public float damageAmount = 50.0f;

    public void StartSkill(Action onSkillEnd = null)
    {
        StartCoroutine(ExecuteAttack(onSkillEnd));
    }

    private IEnumerator ExecuteAttack(Action onSkillEnd)
    {
        // 애니메이션 → Stand (시전 시작)
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Stand");
        }

        // 움직임 멈춤
        var movement = GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.Stop();
        }

        // 시전 시간 대기
        yield return new WaitForSeconds(attackDelay);

        // 애니메이션 → Idle 복귀
        if (animator != null)
        {
            animator.SetTrigger("Idle");
        }

        // 🚀 VFX Graph LightningPrefab 출력
        // 이펙트가 'Instantiate' 되는 순간의 보스 위치에 생성됩니다.
        // 이펙트가 일단 생성되면, 더 이상 위치를 업데이트할 필요가 없습니다.
        GameObject lightning = Instantiate(lightningEffectPrefab, transform.position, Quaternion.identity);

        var vfx = lightning.GetComponent<VisualEffect>();
        if (vfx != null)
        {
            vfx.Play();

            if (vfx.HasFloat("Diameter"))
            {
                vfx.SetFloat("Diameter", attackRadius * 2f);
            }

            if (vfx.HasFloat("Height"))
            {
                vfx.SetFloat("Height", 5f);
            }
        }

        Destroy(lightning, 2f); // 2초 후 자동 삭제 (VFX 이펙트의 재생 시간과 맞춰야 합니다)

        // 딜 적용
        // Physics.OverlapSphere는 현재 'transform.position' (보스의 현재 위치)를 기준으로 합니다.
        // 만약 이펙트가 생성된 '고정된' 위치에서 딜이 들어가야 한다면,
        // 위에서 생성된 lightning 이펙트의 위치를 사용해야 합니다.
        // 현재 코드에서는 보스의 위치를 따라 딜이 들어갑니다. (이게 맞는지는 확인 필요)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius); // <--- 이 부분 확인
        foreach (var hitCollider in hitColliders)
        {
            var player = hitCollider.GetComponent<HealthBar_ES>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }

        // 콜백 (Optional)
        onSkillEnd?.Invoke();
    }

    // 🚀 UpdateLightningPosition 코루틴은 삭제합니다!
    // 이펙트가 보스를 따라다니지 않고 생성 위치에 고정되기를 원하기 때문입니다.
    /*
    private IEnumerator UpdateLightningPosition(Transform lightningTransform, float destroyTime)
    {
        float timer = 0f;
        while (timer < destroyTime)
        {
            if (lightningTransform != null)
            {
                lightningTransform.position = transform.position;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
    */
}