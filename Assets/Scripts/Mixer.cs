using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;


public class Mixer : MonoBehaviour
{
    WaitForSeconds twoSec = new WaitForSeconds(2);
    [SerializeField] MetaballCameraEffect mat1;
    [SerializeField] MetaballCameraEffect mat2;
    [SerializeField] Color mixedColor;
    [SerializeField] KeyScript keyScript;

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
        Debug.Log("color swap!!!");
        Material one = new Material(mat1.cutOutMaterial);
        one.DOColor(mixedColor, 1);
        mat1.cutOutMaterial = one;

        Material two = new Material(mat2.cutOutMaterial);
        two.DOColor(mixedColor, 1);
        mat2.cutOutMaterial = two;

        Invoke("EnableKey",1);
    }

    void EnableKey()
    {
        if (keyScript) keyScript.Usable();
    }
}
