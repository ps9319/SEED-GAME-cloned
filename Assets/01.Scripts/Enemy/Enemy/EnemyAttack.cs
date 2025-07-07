using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    
    private ParticleSystem attackEffect;
    private Animator animator;
    private Weapon weaponComponent;
    private AudioSource audioSource;
    private AudioClip attackSound;
    
    private void Awake()
    {
        weaponComponent = weapon.GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        attackEffect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        attackSound = GetComponent<Enemy>().enemyInfos.attackInfo.attackSound;
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

    public void AttackEffect()
    {
        attackEffect.Play();
        
    }

    public void AttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
