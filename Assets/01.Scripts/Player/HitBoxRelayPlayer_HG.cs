using UnityEngine;

public class HitboxRelayPlayer_HG : MonoBehaviour
{
    private HealthBar_ES health;

    private void Awake()
    {
        health = GetComponentInParent<HealthBar_ES>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HitboxRelay Trigger됨: " + other.name);
        if (health != null)
        {
            health.OnHitboxTriggerEnter(other);
        }
    }
}
