using System.Collections;
using UnityEngine;

public class PlayerAttack_MK : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;

    [SerializeField] private Transform firePoint;  // 발사 위치

    public float speed = 10f;
    public float lifeTime = 2f;

    private Weapon weaponComponent;
    public bool isAttacking = false;
    private float attackCooldown; // 애니메이션 길이만큼
    
    private PlayerMovement movement;
    
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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        // 원거리 공격 입력
        if (Input.GetMouseButtonDown(1))
        {
            ThrowWeapon();
        }

    }

    IEnumerator Attack()
    {
        EnableAttackHitbox();
        // 만약 애니메이션이 
        yield return new WaitForSeconds(attackCooldown);
        DisableAttackHitbox();
    }

    private void ThrowWeapon()
    {
        weapon.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(weapon.gameObject, lifeTime);
    }

    private void EnableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = true;
    }

    private void DisableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = false;
        isAttacking = false;
    }
}
