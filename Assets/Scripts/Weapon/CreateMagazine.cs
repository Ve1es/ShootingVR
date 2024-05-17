using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateMagazine : NetworkBehaviour
{
    [SerializeField] GameObject clonePrefab;

    public void CloneInteractable(SelectExitEventArgs args)
    {
        XRBaseInteractor socket = args.interactor;
        //NetworkObject gameObjectClone = Runner.Spawn(clonePrefab, socket.transform.position, socket.transform.rotation);
        GameObject gameObjectClone = Instantiate(clonePrefab, socket.transform.position, socket.transform.rotation);
        socket.GetComponent<XRSocketInteractor>().StartManualInteraction(gameObjectClone.GetComponent<XRBaseInteractable>());
    }
}

