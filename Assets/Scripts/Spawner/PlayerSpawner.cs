using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private Transform[] _startPoints;
    [SerializeField] private Transform[] _waitingRoomsPoints;
    [SerializeField] private NetworkPrefabRef _playerNetworkPrefab = NetworkPrefabRef.Empty;

    public GameObject Player;

    private void SpawnPlayer(PlayerRef player)
    {
        int index = player.PlayerId % _waitingRoomsPoints.Length;
        Transform spawnPosition = _waitingRoomsPoints[index].transform;
        var playerObject = Runner.Spawn(_playerNetworkPrefab, spawnPosition.position, Quaternion.identity, player);
        Runner.SetPlayerObject(player, playerObject);
        StartCoroutine(CallAfterOneUpdate(spawnPosition, playerObject));
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            SpawnPlayer(player); 
        }
    }
    IEnumerator CallAfterOneUpdate(Transform spawnPosition, NetworkObject networkObject)
    {
        yield return new WaitForEndOfFrame();
        networkObject.GetComponent<NetworkPosition>().HardwareRig.Teleport(spawnPosition);
    }
}
