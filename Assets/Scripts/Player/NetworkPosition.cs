using Fusion;
using UnityEngine;

public class NetworkPosition : NetworkBehaviour
{
     public HardwareRig hardwareRig;
    //[HideInInspector]

    [SerializeField]
    private GameObject _characterModel;
    //[SerializeField]
    //private GameObject _self;


    // As we are in shared topology, having the StateAuthority means we are the local user
    public virtual bool IsLocalNetworkRig => Object && Object.HasStateAuthority;

    public override void Spawned()
    {
        if (IsLocalNetworkRig)
        {
            hardwareRig = FindObjectOfType<HardwareRig>();
            if (hardwareRig == null) Debug.LogError("Missing HardwareRig in the scene");
            _characterModel.SetActive(false);
        }
    }

    public override void FixedUpdateNetwork()
    {
        //Update the rig at each network tick for local player. The NetworkTransform will forward this to other players
        if (IsLocalNetworkRig && hardwareRig)
            {
                //RigState rigState = hardwareRig.RigState;
                ApplyLocalStateToRigParts(hardwareRig.GetTransform());
                //ApplyLocalStateToHandPoses(rigState);
            }
    }

    private void ApplyLocalStateToRigParts(Transform playerTransform)
    {

        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation; 
        //leftHand.transform.position = rigState.leftHandPosition;
        //leftHand.transform.rotation = rigState.leftHandRotation;
        //rightHand.transform.position = rigState.rightHandPosition;
        //rightHand.transform.rotation = rigState.rightHandRotation;
        //headset.transform.position = rigState.headsetPosition;
        //headset.transform.rotation = rigState.headsetRotation;
    }
    //protected virtual void ApplyLocalStateToHandPoses(RigState rigState)
    //{
    //    // we update the hand pose info. It will trigger on network hands OnHandCommandChange on all clients, and update the hand representation accordingly
    //    leftHand.HandCommand = rigState.leftHandCommand;
    //    rightHand.HandCommand = rigState.rightHandCommand;
    //}
}
