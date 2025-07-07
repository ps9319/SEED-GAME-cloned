using UnityEngine;

public class EnemyDropClue : MonoBehaviour
{
    [SerializeField] GameObject clue;
    [SerializeField] private float dropChance = 0.3f;
    
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        if (clue)
        {
            enemyHealth.onDeath += Drop;    
        }
        
    }

    private void OnDisable()
    {
        enemyHealth.onDeath -= Drop;  
    }

    private void Drop()
    {
        if (Random.value < dropChance)
        {
            Instantiate(clue, transform.position, Quaternion.identity);
        }
    }
}
