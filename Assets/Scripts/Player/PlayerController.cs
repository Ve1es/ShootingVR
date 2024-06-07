using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const int MinCurrentRespawnTime = 0;
    private const float DeltaCurrentRespawnTime = 0.5f;

    private bool _isTeleportedPlayer = true;
    private bool _inEndRoom = false;
    private float _currentRespawnTime;

    [SerializeField] private float _respawnTime;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _deathRoom;
    [SerializeField] private Transform _endGameRoom;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _leftLineController;
    [SerializeField] private GameObject _rightLineController;

    public Camera Head;
    public GameObject RightController;
    public GameObject LeftController;
    public GameObject NetworkPlayer;
    public Canvas DeathCanvas;
    public NetworkRoundData RoundData;
    public Vector3 RC;
    public Vector3 LC;
    public Quaternion RCQ;
    public Quaternion LCQ;
    public string NickName;

    public Transform GetTransform() { return gameObject.transform; }

    private void Start()
    {
        _currentRespawnTime = _respawnTime;
        RC = RightController.transform.position;
        LC = LeftController.transform.position;

    }

    public void Update()
    {
        RC = RightController.transform.position;
        LC = LeftController.transform.position;
        RCQ = RightController.transform.rotation;
        LCQ = LeftController.transform.rotation;
        if (_isTeleportedPlayer && RoundData.IsStartGame)
        {
            RespawnTeleport(RoundData.PlayerID);
            _isTeleportedPlayer = false;
            SetPlayerData();
            NetworkPlayer.GetComponent<NetworkPosition>().DrawNickName();
        }
        if (!_inEndRoom && RoundData.IsEndGame)
        {
            TeleportInEndGameRoom();
            _inEndRoom = true;
            if(RoundData.PlayerID>1)
            {
                NetworkPlayer.GetComponent<NetworkPosition>().DespawnPlayer();
            }
        }
        if (RoundData.Time < _currentRespawnTime)
        {
            _currentRespawnTime = RoundData.Time - DeltaCurrentRespawnTime;
            if (_currentRespawnTime < MinCurrentRespawnTime)
                _currentRespawnTime = MinCurrentRespawnTime;
        }
        else
        {
            _currentRespawnTime = _respawnTime;
        }
    }
    private void SetPlayerData()
    {
        RoundData.AddPlayer(NickName);
    }
    public void AddKill(int playerID)
    {
        RoundData.AddKill(playerID);
    }
    public void Teleport(Transform spawnPoint)
    {
        _leftLineController.SetActive(false);
        _rightLineController.SetActive(false);
        _characterController.enabled = false;
        gameObject.transform.position = spawnPoint.position;
        _characterController.enabled = true;
    }
    public void RespawnTeleport()
    {
        _characterController.enabled = false;
        gameObject.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        _characterController.enabled = true;
    }
    public void RespawnTeleport(int playerId)
    {
        _characterController.enabled = false;
        gameObject.transform.position = _spawnPoints[playerId].position;
        _characterController.enabled = true;
    }
    public void TeleportInEndGameRoom()
    {
        RoundData.InNetwork = false;
        _leftLineController.SetActive(true);
        _rightLineController.SetActive(true);
        _characterController.enabled = false;
        gameObject.transform.position = _endGameRoom.position;
        NetworkPlayer.SetActive(false);
        _characterController.enabled = true;
    }
    public void DeathBehaviour(Health healt)
    {
        StartCoroutine(DelayedAction(healt));
    }
    IEnumerator DelayedAction(Health healt)
    {
        DeathCanvas.gameObject.SetActive(true);
        Teleport(_deathRoom);
        RightController.SetActive(false);
        LeftController.SetActive(false);
        yield return new WaitForSeconds(_currentRespawnTime);
        RightController.SetActive(true);
        LeftController.SetActive(true);
        DeathCanvas.gameObject.SetActive(false);
        healt.AddHPRpc();
        RespawnTeleport();
    }

}
