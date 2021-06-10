using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{

    #region Member Variables
    [SerializeField, Header("Just For Identity Of This Timer"),
    Tooltip("Will never use, just for track why we use this timer \n timer title")]
    private string Description;

    [Header("Rendering Setup")]
    [SerializeField, Tooltip("message will print before the time value on the UI screen")]
    private string preMessage = string.Empty;
    [SerializeField, Tooltip("message will print after the time value on the UI screen")]
    private string postMessage = string.Empty;
    [SerializeField, Tooltip("It Will show time value on ui")]
    private TextMeshProUGUI printerText = null;

    [Header("Timer Setup")]
    [SerializeField, Tooltip("If true timer will start on Awake")] 
    private bool startOnAwake = false;
    [SerializeField, Tooltip("If true timer will show how much time left \n otherwise will show continus increase number")]
    bool showLeftTime = true;
    [Tooltip("Counter target time in seconds")]
    public float targetTime = 0;

    [Header("Callback Events")]
    [SerializeField] private UnityEvent OnTimerStart;
    [SerializeField] private UnityEvent OnTimerCounting;
    [SerializeField] private UnityEvent OnTimerOver;
    [SerializeField] private UnityEvent OnTimerPause;
    [SerializeField] private UnityEvent OnTimerResume;
    [SerializeField] private UnityEvent OnTimerStop;

    private Coroutine curretnCounter = null;
    private float time = 0;

    #endregion  Member Variables


    private void Awake()
    {
        if (startOnAwake)
            StartTimer();
    }

    #region Timer Control
    public void StartTimer()
    {
        time = 0;
        OnTimerStart.Invoke();

        StopTimer();
        curretnCounter = StartCoroutine(Counter());
    }


    public void PauseTimer()
    {
        OnTimerPause.Invoke();
        StopTimer();
    }

    public void ResumeTimer()
    {
        OnTimerResume.Invoke();

        StopTimer();
        curretnCounter = StartCoroutine(Counter());
    }


    public void StopTimer()
    {
        OnTimerStop.Invoke();
        if (curretnCounter != null)
            StopCoroutine(curretnCounter);
    }
    #endregion Timer Control

    #region Timer Mechanism
    /// <summary>
    /// actual time counter thread
    /// </summary>
    /// <returns></returns>
    private IEnumerator Counter()
    {
        bool complete = time >= targetTime;
        
        while (!complete)
        {
            yield return new WaitForSeconds(1);
            time++;
            complete = time > targetTime - 1;

            PrepareToRender(time);

            OnTimerCounting.Invoke();
        }
        OnTimerOver.Invoke();
    }


    /// <summary>
    /// preapering time to show forward or inverse
    /// </summary>
    /// <param name="currentTime">Timer current time</param>
    private void PrepareToRender(float currentTime)
    {
        float time = currentTime;
        if(showLeftTime)
        {
            time = targetTime - time;
        }

        string message = preMessage + time + postMessage;
        PrintMessge(message);

    }
    /// <summary>
    /// print message on ui screen
    /// </summary>
    /// <param name="message">timer information</param>
    public void PrintMessge(string message)
    {
        if (printerText != null)
        {
            printerText.text = message;
        }
    }

    #endregion  Timer Mechanism
}
