using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack_MK : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;
    [SerializeField] private ItemToggle_ES ItemToggle;

    private Weapon weaponComponent;
    public bool isAttacking = false;
    private float attackCooldown; // 애니메이션 길이만큼
    
    private PlayerMovement movement;

    //pencilcase 관련
    [SerializeField] private GameObject pencil;
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
        if (movement != null && movement.isRolling) return;
        // 공격 입력
        if (Input.GetMouseButtonDown(0) && !isAttacking && ItemToggle.currentState == "fist")
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }

        // 던지기
        if (Input.GetMouseButtonDown(0) && !isThrown && ItemToggle.currentState == "pencilcase")
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

        pencil.SetActive(true); // 연필 날릴 때만 보이게 함
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

        // 날아간 후 다시 꺼짐
        pencil.SetActive(false);

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
