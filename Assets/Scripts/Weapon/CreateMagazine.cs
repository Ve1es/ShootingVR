using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateMagazine : NetworkBehaviour
{
    [SerializeField] GameObject clonePrefab;
    [SerializeField] XRSocketInteractor interactor;

    public override void Spawned()
    {
        Runner.Spawn(clonePrefab, interactor.transform.position, interactor.transform.rotation);
    }
    public void CloneInteractable(SelectExitEventArgs args)
    {
        XRBaseInteractor socket = args.interactor;
        NetworkObject gameObjectClone = Runner.Spawn(clonePrefab, socket.transform.position, socket.transform.rotation);
        socket.GetComponent<XRSocketInteractor>().StartManualInteraction(gameObjectClone.gameObject.GetComponent<XRBaseInteractable>());
    }
}

