using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoFailDetector : MonoBehaviour
{

    public bool cutedLeftQuad = false;
    public bool cutedRightQuad = false;
    public bool enoughLiquid = false;
    private bool levelFailed = false;


    [SerializeField]
    Timer timer;

    public void OnCutLeftQUad()
    {
        cutedLeftQuad = true;
        LevelFail();
    }

    public void OnCutRightQUad()
    {
        cutedRightQuad = true;
        LevelFail();
    }

    public void OnLiquidFalling(bool enough)
    {
        if (enough)
            timer.StopTimer();
        if (!enoughLiquid)
        { 
            enoughLiquid = enough;
        }
        LevelFail();
    }


    private void LevelFail()
    {
        if (cutedLeftQuad && cutedRightQuad && !enoughLiquid)
        {
            //rock.enabled = false;
            levelFailed = true;
        }
     

        if (cutedLeftQuad && cutedRightQuad && !enoughLiquid && levelFailed)
        {
            timer.StartTimer();
        }
    }
}
