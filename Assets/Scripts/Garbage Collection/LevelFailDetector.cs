using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelFailDetector : MonoBehaviour
{
    [SerializeField] private int numOfRock = 0;

    [SerializeField]
    private UnityEvent OnLevelFail;

    public void OnFailing()
    {
        --numOfRock; 
        Debug.Log("ROCK + " + numOfRock);
        if (numOfRock <= 0)
        {
            OnLevelFail.Invoke();
        }
    }

    public void printMsg(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }
}
