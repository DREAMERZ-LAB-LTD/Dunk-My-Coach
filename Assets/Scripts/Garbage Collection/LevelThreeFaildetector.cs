using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeFaildetector : MonoBehaviour
{
    public bool cutedLeftQuad = false;
    public bool cutedMidQuad = false;
    public bool cutedRightQuad = false;
    private bool levelFailed = false;

    [SerializeField]
    BoxCollider2D rock;
    [SerializeField]
    Timer timer;

    public void OnCutLeftQUad()
    {
        cutedLeftQuad = true;
        LevelFail();
    }
    public void OnCutMidQUad()
    {
        cutedMidQuad = true;
        LevelFail();
    }
    public void OnCutRightQUad()
    {
        cutedRightQuad = true;
        LevelFail();
    }


    private void LevelFail()
    {
        if (cutedLeftQuad && !cutedMidQuad && cutedRightQuad)
        {
            //rock.enabled = false;
            levelFailed = true;
        }
        if (!cutedLeftQuad && cutedMidQuad && cutedRightQuad)
        {
            levelFailed = true;
          
        }
        if (cutedLeftQuad && cutedMidQuad && cutedRightQuad && levelFailed)
        {
            rock.enabled = false;
            timer.StartTimer();
        }
    }
}
