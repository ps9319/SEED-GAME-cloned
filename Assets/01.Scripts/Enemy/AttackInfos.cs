using UnityEngine;

/// <summary>
/// 무기나 적의 공격 수치
/// </summary>

[CreateAssetMenu(fileName = "AttackInfos", menuName = "ScriptableObject/AttackInfos")]
public class AttackInfos : ScriptableObject
{
    public float damage;
    public float attackSpeed;
    public float attackRange;
    
    [Header("공격 효과음")]
    public AudioClip attackSound;

    [Header("모델")]
    public GameObject attackModel;
}
