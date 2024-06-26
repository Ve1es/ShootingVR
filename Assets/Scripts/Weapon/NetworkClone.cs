using Fusion;
using UnityEngine;

public class NetworkClone : NetworkBehaviour
{
    [SerializeField] private MeshRenderer[] _model;
    public GameObject Parent;
    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            foreach (var model in _model) {
                model.enabled = false;
            }
        }
    }
    public override void FixedUpdateNetwork()
    {
        Object.transform.position = Parent.transform.position;
        Object.transform.rotation = Parent.transform.rotation;
    }
}
