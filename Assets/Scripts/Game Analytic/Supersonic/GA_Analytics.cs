/*
using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Analytics : MonoBehaviour
{
    private void Start()
    {
     //   GameAnalytics.SetCustomId("OLY9");
        GameAnalytics.Initialize();

    }

    public void OnLevelStart(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level No : " + levelNo);
    }
    public void OnLevelComplete(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level No : " + levelNo);
    }
    public void OnLevelFail(int levelNo)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level No : " + levelNo);
    }

}
*/