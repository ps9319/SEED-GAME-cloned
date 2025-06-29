using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack_MK : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;

    private Weapon weaponComponent;
    public bool isAttacking = false;
    private float attackCooldown; // 애니메이션 길이만큼
    
    private PlayerMovement movement;

    //pencilcase 관련
    private GameObject spawnedModel;
    public float speed = 10f;
    public float lifeTime = 1f;
    private bool isThrown = false;
    private float throwTimer = 0f;

    private void Awake()
    {
        weaponComponent = weapon.GetComponent<Weapon>();
        attackCooldown = weaponComponent.AttackSpeed;
    }

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // 구르기 중엔 공격 허용 X
        if (movement != null && movement.isRolling && !isThrown) return;
        // 공격 입력
        if (Input.GetMouseButtonDown(0) && !isAttacking && weaponComponent.currentAttackInfo == weaponComponent.attackInfosList[0])
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }

        // 던지기
        if (Input.GetMouseButtonDown(0) && !isThrown && weaponComponent.currentAttackInfo == weaponComponent.attackInfosList[1])
        {
            ThrowWeapon();
            StartCoroutine(ThrowAttack());
        }

        // 던져진 상태면 앞으로 이동
        if (isThrown)
        {
            weapon.transform.position += weapon.transform.forward * speed * Time.deltaTime;

            throwTimer += Time.deltaTime;
            if (throwTimer >= lifeTime)
            {
                // Destroy 대신 비활성화
                weapon.SetActive(false);
                isThrown = false;

                // 무기 되돌리기 (리셋)
                ResetWeapon();
            }
        }
    }

    IEnumerator Attack()
    {
        EnableAttackHitbox();
        // 만약 애니메이션이 
        yield return new WaitForSeconds(attackCooldown);
        DisableAttackHitbox();
    }

    IEnumerator ThrowAttack()
    {
        EnableAttackHitbox();

        float timer = 0f;
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        DisableAttackHitbox();
    }

    private void ThrowWeapon()
    {
        isThrown = true;
        throwTimer = 0f;

        // attackModel 프리팹을 동적으로 생성
        GameObject modelPrefab = weaponComponent.currentAttackInfo.attackModel;
        if (modelPrefab != null)
        {
            spawnedModel = Instantiate(modelPrefab, weapon.transform);
        }

        weapon.transform.SetParent(null); //부모에서 분리
    }

    private void ResetWeapon()
    {
        // 위치와 회전 초기화
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;

        // 타이머 리셋
        throwTimer = 0f;

        // 생성된 모델 제거
        if (spawnedModel != null)
        {
            Destroy(spawnedModel);
            spawnedModel = null;
        }

        // 다시 활성화
        weapon.SetActive(true);
    }



    private void EnableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = true;
    }

    private void DisableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = false;
        isAttacking = false;
        isThrown = false;
    }
}
