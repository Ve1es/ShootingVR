using UnityEngine.SceneManagement;
using Fusion;

public class RestartGame : NetworkBehaviour
{
    public void Restart()
    {
        Runner.Shutdown();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
