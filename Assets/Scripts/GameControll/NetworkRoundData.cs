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
    public bool InNetwork = false;
    public Dictionary<int, int> PlayerKills = new Dictionary<int, int>();
    public Dictionary<int, string> PlayerNiks = new Dictionary<int, string>();
    

    public override void Spawned()
    {
        IsStartGame = false;
        InNetwork = true;
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
        if (!PlayerKills.ContainsKey(playerID))
        {
            PlayerKills[playerID] = KillsAmountBeforeKill;
        }
        if (!PlayerNiks.ContainsKey(playerID))
        {
            PlayerNiks[playerID] = playerName;
        }
    }
    public void AddKill(int player)
    {
            Rpc_UpdateKillCount(player);
    }
    [Rpc]
    private void Rpc_UpdateKillCount(int playerID)
    {
        if (PlayerKills.ContainsKey(playerID))
        {
            PlayerKills[playerID]++;
            
        }
        else
        {
            PlayerKills[playerID] = KillsAmountAfterOneKill;
        }
        if (PlayerID == playerID)
        {
            LocalPlayerKills++;
        }
    }  
}
