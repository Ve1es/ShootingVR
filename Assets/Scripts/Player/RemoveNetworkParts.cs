using Fusion;
using UnityEngine;

public class RemoveNetworkParts : NetworkBehaviour
{
    [SerializeField] Collider _bodyCollider;
    [SerializeField] SkinnedMeshRenderer _bodyMesh;
    [SerializeField] MeshRenderer _rHand;
    [SerializeField] MeshRenderer _lHand;
    public virtual bool IsLocalNetworkRig => Object && Object.HasStateAuthority;
    public override void Spawned()
    {
        if (IsLocalNetworkRig)
        {
            _bodyCollider.enabled = false;
            _bodyMesh.enabled = false;
            _rHand.enabled = false;
            _lHand.enabled = false;
        }
    }
}
