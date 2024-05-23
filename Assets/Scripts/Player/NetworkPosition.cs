using Fusion;
using UnityEngine;

public class NetworkPosition : NetworkBehaviour
{
    private Vector3 _offset = new Vector3(0, -1.5f, 0); 

    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _characterModel;
    [SerializeField] private GameObject _nickname;
    [SerializeField] private Transform _nicknamePosition;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _head;

    [Networked] public string PlayerName { get; set; }

    public virtual bool IsLocalNetworkRig => Object && Object.HasStateAuthority;
    public PlayerController HardwareRig;
    public PlayerUIController PlayerUIController;

    public override void Spawned()
    {
        if (IsLocalNetworkRig)
        {
            HardwareRig = FindObjectOfType<PlayerController>();
            PlayerUIController = FindObjectOfType<PlayerUIController>();
            if (HardwareRig == null) Debug.LogError("Missing HardwareRig in the scene");
            if (PlayerUIController == null) Debug.LogError("Missing PlayerUIController in the scene");
            {
                PlayerUIController._weaponController = _weaponController;
                PlayerUIController._health = _health;
            }
            HardwareRig.RoundData.PlayerID = Object.StateAuthority.PlayerId;
            HardwareRig.NetworkPlayer = gameObject;
            PlayerName = HardwareRig.NickName;
        }
    }
    public void DrawNickName()
    {
        RPC_SpawnNickName();
    }
    [Rpc]
    public void RPC_SpawnNickName()
    {
        GameObject nickName = Instantiate(_nickname, _nicknamePosition.position, Quaternion.identity);
        nickName.GetComponent<Nickname>().SetNickname(PlayerName);
        nickName.GetComponent<Nickname>()._playerParent = gameObject.transform;
    }
    public override void FixedUpdateNetwork()
    {
        if (IsLocalNetworkRig && HardwareRig)
        {
            ApplyLocalStateToRigParts();
        }
    }

    private void ApplyLocalStateToRigParts()
    {
        transform.position = HardwareRig.Head.transform.position+ _offset;
        transform.rotation = HardwareRig.transform.rotation;
        _leftHand.transform.position = HardwareRig.LeftController.transform.position;
        _leftHand.transform.rotation = HardwareRig.LeftController.transform.rotation;
        _rightHand.transform.position = HardwareRig.RightController.transform.position;
        _rightHand.transform.rotation = HardwareRig.RightController.transform.rotation;
        _head.transform.eulerAngles = new Vector3(_head.transform.eulerAngles.x, HardwareRig.Head.transform.eulerAngles.y, _head.transform.eulerAngles.z);
    }  
}
