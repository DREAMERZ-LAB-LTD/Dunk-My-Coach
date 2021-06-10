using UnityEngine;
//using LionStudios;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;

public class LionsKitAnaytics : MonoBehaviour
{
    private void Start()
    {
        //Analytics.Events.LevelStarted(SceneManager.GetActiveScene());
        
        GameAnalytics.Initialize();
        int levelNo = SceneManager.GetActiveScene().buildIndex;
        OnLevelStart(levelNo);
    }

    public void Restart()
    {
        //Analytics.Events.LevelRestart(SceneManager.GetActiveScene());
        int levelNo = SceneManager.GetActiveScene().buildIndex;
        OnLevelStart(levelNo);
    }

    public void LevelCompleted()
    {
        //Analytics.Events.LevelComplete(SceneManager.GetActiveScene());
        int levelNo = SceneManager.GetActiveScene().buildIndex;
        OnLevelComplete(levelNo);
    }


    public void Fail()
    {
        int levelNo = SceneManager.GetActiveScene().buildIndex;
        OnLevelFail(levelNo);
    }



    #region Supresonic Analytics SDK
    private void OnLevelStart(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level No : " + levelNo);
    }
    private void OnLevelComplete(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level No : " + levelNo);
    }
    private void OnLevelFail(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level No : " + levelNo);
    }
    #endregion Supresonic Analytics SDK
}
