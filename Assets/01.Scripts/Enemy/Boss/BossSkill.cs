using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public GameObject skill1Prefab;
    public GameObject skill2Prefab;

    public Transform firePoint;
    public float skillCooldown = 10f;
    private float lastSkillTime = -Mathf.Infinity;

    private bool isUsingSkill = false;

    public bool IsSkillInProgress()
    {
        return isUsingSkill;
    }

    public bool IsCooldownOver()
    {
        return Time.time >= lastSkillTime + skillCooldown;
    }

    public bool CheckSkillChance(float chance)
    {
        return Random.value < chance;
    }


    public void TryCastSkill1()
    {

        isUsingSkill = true;
        lastSkillTime = Time.time;

        // 🔥 Skill1 오브젝트 생성 및 시전
        Vector3 spawnPos = transform.position + transform.forward * 5f;
        spawnPos.y = 0f;

        GameObject obj = Instantiate(skill1Prefab, spawnPos, skill1Prefab.transform.rotation);
        BossSkill1 skill = obj.GetComponent<BossSkill1>();
        if (skill != null)
            skill.StartSkill();

        Invoke(nameof(EndSkill), 2f); // 스킬 애니메이션 끝나는 시간에 맞춰
    }

    public void TryCastSkill2()
    {

        isUsingSkill = true;
        lastSkillTime = Time.time;

        // 🔥 Skill2 (레이저) 오브젝트 생성 및 firePoint 기준 위치
        GameObject obj = Instantiate(skill2Prefab, firePoint.position, Quaternion.identity);
        BossSkill2 skill = obj.GetComponent<BossSkill2>();
        if (skill != null)
        {
            skill.firePoint = firePoint;
            skill.StartSkill();
        }

        Invoke(nameof(EndSkill), 2f);
    }

    private void EndSkill()
    {
        isUsingSkill = false;
    }
    
    public void PuzzleSuccess()
    {
        Debug.Log("퍼즐 성공 → 즉사 스킬 취소, 전투 계속! 💪");
        // 예: 즉사 애니메이션 취소, 전투 로직 복귀 등
    }

    public void PuzzleFailed()
    {
        Debug.Log("퍼즐 실패 → 플레이어 즉사! ☠️");
        //FindObjectOfType<PlayerMovement>().Die();
    }
}
