using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfos infos;

    public static event Action<Enemy> onEnemyDied;
    public EnemyInfos enemyInfos => infos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke(nameof(die), 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void die()
    {
        onEnemyDied?.Invoke(this);
        Destroy(this.gameObject);
    }
}
