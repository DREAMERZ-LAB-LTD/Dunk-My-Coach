using UnityEngine;
using LionStudios;
using UnityEngine.SceneManagement;

public class LionsKitAnaytics : MonoBehaviour
{
    private void Start()
    {
        Analytics.Events.LevelStarted(SceneManager.GetActiveScene());
    }

    public void Restart()
    {
        Analytics.Events.LevelRestart(SceneManager.GetActiveScene());
    }

    public void LevelCompleted()
    {
        Analytics.Events.LevelComplete(SceneManager.GetActiveScene());
    }
}
