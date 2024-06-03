using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateMagazine : NetworkBehaviour
{
    [SerializeField] private GameObject _clonePrefab;
    [SerializeField] private XRSocketInteractor _interactor;
    [SerializeField] private Transform _spawnPoint;

    public override void Spawned()
    {
        NetworkObject gameObjectClone = Runner.Spawn(_clonePrefab, Object.transform.position, Object.transform.rotation);
        _interactor.startingSelectedInteractable = gameObjectClone.GetComponent<XRGrabInteractable>();
    }
    public void CloneInteractable(SelectExitEventArgs args)
    {
        XRBaseInteractor socket = args.interactor;
        NetworkObject gameObjectClone = Runner.Spawn(_clonePrefab, socket.transform.position, socket.transform.rotation);
        socket.GetComponent<XRSocketInteractor>().StartManualInteraction(gameObjectClone.gameObject.GetComponent<XRBaseInteractable>());
    }
}

