using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;
using TMPro;

public class Mixer : MonoBehaviour
{
    WaitForSeconds twoSec = new WaitForSeconds(2);
    [SerializeField] MetaballCameraEffect mat1;
    [SerializeField] MetaballCameraEffect mat2;
    [SerializeField] Color mixedColor;
    [SerializeField] Color mixedOutlineColor;
    [SerializeField] public int minCount;
    [SerializeField] LayerMask cuttableLayerMask;
    [SerializeField] GameObject wood;
    [SerializeField] TextMeshPro warningLabel;
    [SerializeField] Texture bodyTexture;

    public LevelTwoFailDetector failDetector;
    int count;
    bool mixed = false;
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
        if (count > minCount && !mixed)
        {
            mixed = true;
            TriggerColorSwap();

            wood.layer = LayerMask.NameToLayer("Cuttable");
            warningLabel.DOFade(0, 1);
            failDetector.OnLiquidFalling(true);
        }
        else if(count < minCount && mixed)
        {
            failDetector.OnLiquidFalling(false);
        }
    }

    void TriggerColorSwap()
    {
        Debug.Log("color swap!!!");
        Material one = new Material(mat1.cutOutMaterial);
        one.DOColor(mixedColor, 1);
        mat1.cutOutMaterial = one;
        StartCoroutine(SwapOutline(one));

        Material two = new Material(mat2.cutOutMaterial);
        two.DOColor(mixedColor, 1);
        mat2.cutOutMaterial = two;
        StartCoroutine(SwapOutline(two));
    }

    IEnumerator SwapOutline(Material mat)
    {
        Color first = mat.GetColor("_StrokeColor");
        float ElapsedTime = 0.0f;
        float TotalTime = 2.0f;
        while (ElapsedTime < TotalTime)
        {
            ElapsedTime += Time.deltaTime;
            mat.SetColor("_StrokeColor", Color.Lerp(first, mixedOutlineColor, (ElapsedTime / TotalTime)));
            yield return null;
        }
    }
}
