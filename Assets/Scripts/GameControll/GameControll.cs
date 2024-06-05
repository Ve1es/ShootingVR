using Fusion;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameControll : NetworkBehaviour, IPlayerLeft
{
    enum GamePhase
    {
        Starting,
        Running,
        Ending,
        Exit
    }
    private const int OnePlayerWin = 1;
    private const int TwoPlayerWin = -1;

    private static GameControll _singleton;
    private bool _playerLeft=false;
    [Networked] private GamePhase Phase { get; set; }
    [Networked] private TickTimer Timer { get; set; }
    
    [SerializeField] private int _spawnDelay = 30;
    [SerializeField] private int _roundDelay = 300;
    [SerializeField] private NetworkRoundData _roundData;
    [SerializeField] private TMP_Text _result;
    [SerializeField] private TMP_Text _teamWin;

    
    public static GameControll Singleton
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
        if (Object.HasStateAuthority)
        {
            Phase = GamePhase.Starting;
            Timer = TickTimer.CreateFromSeconds(Runner, _spawnDelay);
            _roundData.TimerName = "Разминка: ";
        }
    }
    public override void Render()
    {
        switch (Phase)
        {
            case GamePhase.Starting:
                UpdateStartingDisplay();
                break;
            case GamePhase.Running:
                UpdateRunningDisplay();
                break;
            case GamePhase.Ending:
                UpdateEndingDisplay();
                break;
        }
        if(_playerLeft)
        {
            Phase = GamePhase.Ending;
        }
    }
    public void UpdateStartingDisplay()
    {
        _roundData.Time = Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0);
        if (!Object.HasStateAuthority)
            return;
        if (!Timer.Expired(Runner))
            return;
        if (Runner.ActivePlayers.Count() > 1)
        {
            _roundData.StartGame();
            Timer = TickTimer.CreateFromSeconds(Runner, _roundDelay);
            _roundData.TimerName = "Раунд: ";
            Phase = GamePhase.Running;
        }
    }
    public void UpdateRunningDisplay()
    {
        _roundData.Time = Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0);
        if (Timer.Expired(Runner))
            Phase = GamePhase.Ending;     
    }
    public void UpdateEndingDisplay()
    {
        EndGameResult();
        if (!Object.HasStateAuthority) return;
        Phase = GamePhase.Exit;
    }
    public void EndGameResult()
    {
        if (_playerLeft)
        {
            _teamWin.text = "Ваша команда победила!";
        }
        else
        {
            if (FindMaxValueKey() == TwoPlayerWin)
            { _teamWin.text = "Ничья"; }
            else
            {
                if (_roundData.PlayerID == FindMaxValueKey())
                {
                    _teamWin.text = "Ваша команда победила!";
                }
                else
                {
                    _teamWin.text = "Ваша команда проиграла!";
                }
            }

        }
        string output = "";
        foreach (var kvp in _roundData.PlayerKills)
        {
            output += $"{_roundData.PlayerNiks[kvp.Key]}: {kvp.Value} килов\n";
        }
        _result.text = output;
        _roundData.IsEndGame = true;
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (Phase != GamePhase.Ending&& Phase != GamePhase.Exit)
        {
            _playerLeft = true;
        }
    }
    public int FindMaxValueKey()
    {
        int maxValue = _roundData.PlayerKills.Values.Max();
        var maxKeys = _roundData.PlayerKills.Where(pair => pair.Value == maxValue).Select(pair => pair.Key).ToList();

        if (maxKeys.Count == OnePlayerWin)
        {
            return maxKeys.First();
        }
        else
        {
            return TwoPlayerWin;
        }
    }
}
