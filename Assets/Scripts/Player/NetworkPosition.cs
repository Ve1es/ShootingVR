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
    public Vector3 LeftHand;
    public Vector3 RightHand;
    public Quaternion LeftHandQ;
    public Quaternion RightHandQ;

    public override void Spawned()
    {
        if (IsLocalNetworkRig)
        {
            HardwareRig = FindObjectOfType<PlayerController>();
            PlayerUIController = FindObjectOfType<PlayerUIController>();
            if (HardwareRig == null) Debug.LogError("Missing HardwareRig in the scene");
            if (PlayerUIController == null) Debug.LogError("Missing PlayerUIController in the scene");
            {
                PlayerUIController.WeaponController = _weaponController;
                PlayerUIController.Health = _health;
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
        nickName.GetComponent<Nickname>().PlayerParent = gameObject.transform;
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
        transform.rotation = new Quaternion(0, HardwareRig.Head.transform.rotation.y, 0, HardwareRig.Head.transform.rotation.w);

        _leftHand.transform.position = HardwareRig.LC;
        _leftHand.transform.rotation = HardwareRig.LCQ;
        _rightHand.transform.position = HardwareRig.RC;
        _rightHand.transform.rotation = HardwareRig.RCQ;
    }
    public void DespawnPlayer()
    {
        Runner.Shutdown();
    }
}
