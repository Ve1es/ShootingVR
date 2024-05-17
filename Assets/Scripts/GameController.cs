using Fusion;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class GameController: NetworkBehaviour
{
    enum GamePhase
    {
        Starting,
        Running,
        Ending
    }

    [SerializeField] private float _startDelay = 4.0f;
    [SerializeField] private float _endDelay = 4.0f;
    [SerializeField] private float _gameSessionLength = 180.0f;

    //[SerializeField] private TextMeshProUGUI _startEndDisplay;
    //[SerializeField] private TextMeshProUGUI _ingameTimerDisplay;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private NetworkRoundData _networkRoundData;

    [Networked] private TickTimer Timer { get; set; }
    [Networked] private GamePhase Phase { get; set; }

    public bool GameIsRunning => Phase == GamePhase.Running;

    private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();

    private static GameController _singleton;

    public static GameController Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton != null)
            {
                throw new InvalidOperationException();
            }
            _singleton = value;
        }
    }

    private void Awake()
    {
        GetComponent<NetworkObject>().Flags |= NetworkObjectFlags.MasterClientObject;
        Singleton = this;
    }

    private void OnDestroy()
    {
        if (Singleton == this)
        {
            _singleton = null;
        }
        else
        {
            throw new InvalidOperationException();
        }

    }

    public override void Spawned()
    {
        //_startEndDisplay.gameObject.SetActive(true);
        //_ingameTimerDisplay.gameObject.SetActive(false);
        //_playerOverview.Clear();

        if (Object.HasStateAuthority)
        {
            // Initialize the game state on the master client
            Phase = GamePhase.Starting;
            Timer = TickTimer.CreateFromSeconds(Runner, _startDelay);
        }
    }

    public override void Render()
    {
        // Update the game display with the information relevant to the current game phase
        switch (Phase)
        {
            case GamePhase.Starting:
                UpdateStartingDisplay();
                break;
            case GamePhase.Running:
                UpdateRunningDisplay();
                if (HasStateAuthority)
                {
                    CheckIfGameHasEnded();
                }
                break;
            case GamePhase.Ending:
                UpdateEndingDisplay();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateStartingDisplay()
    {
        // --- All clients
        // Display the remaining time until the game starts in seconds (rounded down to the closest full second)
        //_startEndDisplay.text = $"Game Starts In {Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0)}";

        if (!Object.HasStateAuthority)
            return;

        if (!Timer.Expired(Runner)) 
            return;

        // --- Master client
        // Starts the Spaceship spawner once the game start delay has expired
       // _playerSpawner.StartPlayerSpawner();

        // Switches to the Running GameState and sets the time to the length of a game session
        Phase = GamePhase.Running;
        Timer = TickTimer.CreateFromSeconds(Runner, _gameSessionLength);
    }

    private void UpdateRunningDisplay()
    {
        // --- All clients
        // Display the remaining time until the game ends in seconds (rounded down to the closest full second)
        //_startEndDisplay.gameObject.SetActive(false);
        //_ingameTimerDisplay.gameObject.SetActive(true);
        //_ingameTimerDisplay.text = $"{Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0).ToString("000")} seconds left... Player RTT: {(int)(1000 * Runner.GetPlayerRtt(Runner.LocalPlayer))}ms";
    }

    private void UpdateEndingDisplay()
    {
        // --- All clients
        // Display the results and
        // the remaining time until the current game session is shutdown

        //_startEndDisplay.gameObject.SetActive(true);
        //_ingameTimerDisplay.gameObject.SetActive(false);
        //поменять под мою игру
        //_startEndDisplay.text = $"{playerData.NickName} won with {playerData.Score} points. Disconnecting in {Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0)}";

        // Shutdowns the current game session.
        if (Timer.Expired(Runner))
            Runner.Shutdown();
    }

    public void CheckIfGameHasEnded()
    {
        // --- Master client

        if (Timer.ExpiredOrNotRunning(Runner))
        {
            GameHasEnded();
            return;
        }



        //int playersAlive = 0;

        //for (int i = 0; i < _playerDataNetworkedIds.Count; i++)
        //{
        //    if (Runner.TryFindBehaviour(_playerDataNetworkedIds[i],
        //            out PlayerDataNetworked playerDataNetworkedComponent) == false)
        //    {
        //        _playerDataNetworkedIds.RemoveAt(i);
        //        i--;
        //        continue;
        //    }

        //    if (playerDataNetworkedComponent.Lives > 0) playersAlive++;
        //}


        // If more than 1 player is left alive, the game continues.
        // If only 1 player is left, the game ends immediately.
        if (Runner.ActivePlayers.Count() == 1) return;

        foreach (var playerDataNetworkedId in _playerDataNetworkedIds)
        {

            //Winner = playerDataNetworkedId;
        }

        GameHasEnded();
    }

    private void GameHasEnded()
    {
        Timer = TickTimer.CreateFromSeconds(Runner, _endDelay);
        Phase = GamePhase.Ending;
    }

    public void TrackNewPlayer(NetworkBehaviourId playerDataNetworkedId)
    {
        _playerDataNetworkedIds.Add(playerDataNetworkedId);
    }
}
