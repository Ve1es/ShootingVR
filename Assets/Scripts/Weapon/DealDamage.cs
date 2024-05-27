using Fusion;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private const float Default_Damage = 10;
    private float _damage = Default_Damage;

    public int PlayerID;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("OnTriggerEnter Соприкосновение с объектом: " + other.gameObject.name);
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamageRpc(_damage, PlayerID);
        }
        Runner.Despawn(Object);
    }
}
