using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;


public class DefeatCheck : MonoBehaviour
{
    [SerializeField] GameObject defeatUI;
    WaitForSeconds twoSec = new WaitForSeconds(2);
    [SerializeField] public int minCount;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TriggerOnOff());
    }


    IEnumerator TriggerOnOff()
    {
        yield return twoSec;
        GetComponent<BoxCollider2D>().enabled = false;
        count = 0;
        yield return twoSec;
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(TriggerOnOff());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
        if (count > minCount)
        {
            TriggerColorSwap();
        }
    }

    void TriggerColorSwap()
    {
        defeatUI.SetActive(true);
    }

   
}
