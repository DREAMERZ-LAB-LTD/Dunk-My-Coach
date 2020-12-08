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
    [SerializeField] public int minCount;
    [SerializeField] LayerMask cuttableLayerMask;
    [SerializeField] GameObject wood;
    [SerializeField] TextMeshPro warningLabel;
    [SerializeField] Texture bodyTexture;
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
    }
}
