using Fusion;
using System.Collections.Generic;
public class NetworkRoundData : NetworkBehaviour
{
    private const int KillsAmountAfterOneKill = 1;
    private const int KillsAmountBeforeKill = 0;

    [Networked] public int Time { get; set; } = 0;
    [Networked] public int LastSpawnPoint { get; set; } = -1;
    [Networked] public string TimerName { get; set; }
    [Networked] public bool IsStartGame { get; set; }
    [Networked] public bool IsEndGame { get; set; }
    
    public int PlayerID;
    public int LocalPlayerKills = 0;
    public string LocalPlayerNick;
    public Dictionary<int, int> playerKills = new Dictionary<int, int>();
    public Dictionary<int, string> playerNiks = new Dictionary<int, string>();
    

    public override void Spawned()
    {
        IsStartGame = false;
    }
   
    public void StartGame()
    {
        RPC_Start();
    }
    public void EndGame()
    {
        RPC_End();
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Start()
    {
        IsStartGame = true;
    }
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_End()
    {
        IsStartGame = false;
        IsEndGame = true;
    }
    public void AddPlayer(string playerName)
    {
        Rpc_UpdatePlayerName(Runner.LocalPlayer.PlayerId, playerName);
        LocalPlayerNick = playerName;
        PlayerID = Runner.LocalPlayer.PlayerId;
    }
    [Rpc]
    private void Rpc_UpdatePlayerName(int playerID, string playerName)
    {
        if (!playerKills.ContainsKey(playerID))
        {
            playerKills[playerID] = KillsAmountBeforeKill;
        }
        if (!playerNiks.ContainsKey(playerID))
        {
            playerNiks[playerID] = playerName;
        }
    }
    public void AddKill(int player)
    {
            Rpc_UpdateKillCount(player);
    }
    [Rpc]
    private void Rpc_UpdateKillCount(int playerID)
    {
        if (playerKills.ContainsKey(playerID))
        {
            playerKills[playerID]++;
            
        }
        else
        {
            playerKills[playerID] = KillsAmountAfterOneKill;
        }
        if (PlayerID == playerID)
        {
            LocalPlayerKills++;
        }
    }
}
