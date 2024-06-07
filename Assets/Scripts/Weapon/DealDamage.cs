using Fusion;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private const float Default_Damage = 10;

    private float _damage = Default_Damage;

    public int PlayerID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamageRpc(_damage, PlayerID);
        }
        if (!other.TryGetComponent(out PlayerController player) && !other.TryGetComponent(out WeaponController weapon) && !other.TryGetComponent(out Bullet bullet))
        {
            Debug.LogError(other.name);
            Runner.Despawn(Object);
        }
    }
}
