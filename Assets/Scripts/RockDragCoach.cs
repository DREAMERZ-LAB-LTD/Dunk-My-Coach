using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RockDragCoach : MonoBehaviour
{
    [SerializeField] AudioClip rip;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject);
        if (col.gameObject.GetComponent<CoachCollider>())
        {
            GetComponent<AudioSource>().PlayOneShot(rip);
            col.gameObject.transform.SetParent(transform);
            col.transform.DOLocalMove(new Vector3(-0.000235f, -0.00453f, 0), 0.1f);
        }
    }
}
