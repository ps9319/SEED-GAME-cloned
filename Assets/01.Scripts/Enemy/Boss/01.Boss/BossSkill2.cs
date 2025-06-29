using System.Collections;
using UnityEngine;

public class BossSkill2 : MonoBehaviour
{
    public Transform firePoint;               // 레이저 시작 위치
    public Transform target;                  // 대상 (플레이어)
    public GameObject laserPrefab;            // 레이저 비주얼 프리팹
    public float laserDuration = 3f;          // 레이저 유지 시간
    public float laserLength = 20f;           // 최대 사거리
    public float damage = 50f;                // 데미지 양
    public LayerMask hitLayerMask;            // 데미지 판정용 레이어

    public void StartSkill()
    {
        // 플레이어 자동 할당
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                target = player.transform;
        }

        if (target != null && firePoint != null)
            StartCoroutine(FireLaser());
    }

    private IEnumerator FireLaser()
    {
        Vector3 startPos = firePoint.position;
        Vector3 direction = Quaternion.Euler(0f, -15f, 0f) * (target.position - startPos).normalized;


        // 레이저 이펙트 생성
        GameObject laser = Instantiate(laserPrefab, startPos, Quaternion.LookRotation(direction));
        

        // 충돌 판정 및 피격 위치 이펙트 이동
        if (Physics.Raycast(startPos, direction, out RaycastHit hit, laserLength, hitLayerMask))
        {
            // 1. 데미지 적용
            var player = hit.collider.GetComponent<HealthBar_ES>();
            if (player != null)
                player.TakeDamage(damage);

            // 2. 히트 이펙트 위치 이동
            Transform hitEffect = laser.transform.Find("Beam/Hit 2");
            if (hitEffect != null)
            {
                hitEffect.position = hit.point;
            }
            else
            {
                Debug.LogWarning("⚠️ Hit 2 이펙트를 찾을 수 없습니다.");
            }
        }

        Destroy(laser, laserDuration);
        yield return new WaitForSeconds(laserDuration);
        Destroy(gameObject); // BossSkill2(Clone) 오브젝트 삭제
    }
}