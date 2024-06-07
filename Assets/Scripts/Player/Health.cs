using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    private const float MaxHP=100;
    [SerializeField] private NetworkPosition _networkPosition;
    [Networked] public float NetworkedHealth { get; set; } = 100;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage, int playerID)
    {
        if (NetworkedHealth > 0)
        {
            NetworkedHealth -= damage;
            if (NetworkedHealth <= 0)
            {
                _networkPosition.HardwareRig.AddKill(playerID);
                _networkPosition.HardwareRig.DeathBehaviour(this);
            }
        }
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void AddHPRpc()
    {
        NetworkedHealth = MaxHP;
    }
}
