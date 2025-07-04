using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;
    
    private Weapon weaponComponent;

    private void Awake()
    {
        weaponComponent = weapon.GetComponent<Weapon>();
    }

    // animator로 공격중인지 판단, 그 시간동안 hitbox를 키자
    private void Update()
    {
        // 애니메이션 Attack이 재생 중이면
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            EnableAttackHitbox();
            return;
        }
        DisableAttackHitbox();
    }

    private void EnableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = true;
    }

    private void DisableAttackHitbox()
    {
        weaponComponent.Hitbox.enabled = false;
    }
}
