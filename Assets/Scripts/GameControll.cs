using Fusion;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameControll : NetworkBehaviour
{
    enum GamePhase
    {
        Starting,
        Running,
        Ending
    }
    [Networked] private GamePhase Phase { get; set; }
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private int _spawnDelay = 5;
    public override void FixedUpdateNetwork()
    {
        switch (Phase)
        {
            case GamePhase.Starting:
                UpdateStartingDisplay();
                break;
            case GamePhase.Running:
                //UpdateRunningDisplay();
                break;
            case GamePhase.Ending:
                //UpdateEndingDisplay();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void UpdateStartingDisplay()
    {
        if(Runner.ActivePlayers.Count() > 0)
        {
            Phase = GamePhase.Running;
        }
    }

    private void TeleportPlayer()
    {
        _playerSpawner.TeleportPlayerToSpawnPosition();
    }
    //public void RespawnPlayer()
    //{
    //    StartCoroutine(RespawnWait());
    //}
    //IEnumerator RespawnWait()
    //{
    //    yield return new WaitForSeconds(_spawnDelay);
    //    TeleportPlayer();
    //}

}
