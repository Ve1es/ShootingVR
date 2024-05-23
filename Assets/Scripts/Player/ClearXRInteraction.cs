using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClearXRInteraction : NetworkBehaviour
{
    [SerializeField] private XRSimpleInteractable _simpleInteractable;
    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private XRGrabInteractable _grabberInteractable;
    public virtual bool IsLocalNetworkRig => Object && Object.HasStateAuthority;

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
        {
            if (_simpleInteractable != null)
                _simpleInteractable.enabled = false;
            if (_socketInteractor != null)
                _socketInteractor.enabled = false;
            if (_grabberInteractable != null)
                _grabberInteractable.enabled = false;
        }
    }
}
