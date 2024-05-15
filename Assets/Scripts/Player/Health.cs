using Fusion;

public class Health : NetworkBehaviour
{
    public NetworkObject DeletedObject;
    [Networked]
    public float NetworkedHealth { get; set; } = 100;

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage)
    {
        NetworkedHealth -= damage;
        if (NetworkedHealth <= 0)
        {
            Runner.Despawn(DeletedObject);
        }
    }
}
