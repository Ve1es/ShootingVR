using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private NetworkRunner _runner;
    [SerializeField] private TMP_InputField _roomName = null;
    
    public void StartShared()
    {
        StartGame(GameMode.Shared, _roomName.text);
    }

    private async void StartGame(GameMode mode, string roomName)
    {
        _runner = FindObjectOfType<NetworkRunner>();
        if (_runner == null)
        {
            _runner = Instantiate(_runner);
        }

        // Let the Fusion Runner know that we will be providing user input
        _runner.ProvideInput = true;

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomName,
            Scene = scene,
            //ObjectProvider = _runner.GetComponent<NetworkObjectPoolDefault>(),
        };

        await _runner.StartGame(startGameArgs);
    }
}
