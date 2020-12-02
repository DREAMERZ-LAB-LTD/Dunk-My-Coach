using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomRotateAnimation : MonoBehaviour
{
    public Vector3 targetRotationMin; 
    public Vector3 targetRotationMax; 

    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(Random.Range(targetRotationMin.x, targetRotationMax.x), Random.Range(targetRotationMin.y, targetRotationMax.y), Random.Range(targetRotationMin.z, targetRotationMax.z)),Random.Range(1,5)).SetLoops(-1, LoopType.Yoyo);
    }   
}
