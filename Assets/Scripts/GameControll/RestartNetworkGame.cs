using UnityEngine.SceneManagement;
using Fusion;

public class RestartNetworkGame : NetworkBehaviour
{
    public void Restart()
    {
        Runner.Shutdown();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
