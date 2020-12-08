using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowSound : MonoBehaviour
{
    [SerializeField] AudioClip waterFlowSound;
    bool wet;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (wet)
            return;
        wet = true;
        GetComponent<AudioSource>().PlayOneShot(waterFlowSound, Random.Range(0.25f,0.5f));
    }
}
