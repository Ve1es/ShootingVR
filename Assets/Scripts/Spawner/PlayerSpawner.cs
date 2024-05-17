using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private Transform[] _startPoints;
    [SerializeField] private NetworkPrefabRef _playerNetworkPrefab = NetworkPrefabRef.Empty;
    public GameObject Player;

    private void SpawnPlayer(PlayerRef player)
    {
        int index = player.PlayerId % _startPoints.Length;
        Transform spawnPosition = _startPoints[index].transform;
        var playerObject = Runner.Spawn(_playerNetworkPrefab, spawnPosition.position, Quaternion.identity, player);
        Runner.SetPlayerObject(player, playerObject);
        Player.GetComponent<HardwareRig>().Teleport(spawnPosition);
    }

    public void PlayerJoined(PlayerRef player)
    {
        Debug.LogError(player);
        if (player == Runner.LocalPlayer)
        {
            SpawnPlayer(player); 
        }
    }

    public void TeleportPlayerToSpawnPosition()
    {
        Player.GetComponent<HardwareRig>().Teleport(_startPoints[Random.Range(0, _startPoints.Length)]);
    }
}
