using Fusion;

public class NetworkRoundData : NetworkBehaviour
{
    [Networked] public int Time { get; set; } = 0;
    [Networked] public int SpawnPointNumber { get; set; } = 0;
    public int Kills { get; set; } = 0;
}
