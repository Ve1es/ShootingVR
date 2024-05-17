using Fusion;
using UnityEngine;

public class NetworkPosition : NetworkBehaviour
{
    public HardwareRig HardwareRig;
    public PlayerUIController PlayerUIController;
    [SerializeField] private GameObject _characterModel;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private Health _health;
    public virtual bool IsLocalNetworkRig => Object && Object.HasStateAuthority;

    public override void Spawned()
    {
        if (IsLocalNetworkRig)
        {
            HardwareRig = FindObjectOfType<HardwareRig>();
            PlayerUIController = FindObjectOfType<PlayerUIController>();
            if (HardwareRig == null) Debug.LogError("Missing HardwareRig in the scene");
            _characterModel.SetActive(false);
            if (PlayerUIController == null) Debug.LogError("Missing PlayerUIController in the scene");
            {
                PlayerUIController._weaponController = _weaponController;
                PlayerUIController._health = _health;
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (IsLocalNetworkRig && HardwareRig)
        {
            ApplyLocalStateToRigParts(HardwareRig.GetTransform());
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
}
