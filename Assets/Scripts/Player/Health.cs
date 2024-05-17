using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private NetworkPosition _networkPosition;
    [Networked] public float NetworkedHealth { get; set; } = 100;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage)
    {
        NetworkedHealth -= damage;
        if (NetworkedHealth <= 0)
        {
            _networkPosition.HardwareRig.RespawnTeleport();
        }
    }
}
