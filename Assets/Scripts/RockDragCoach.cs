using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class RockDragCoach : MonoBehaviour
{
    [SerializeField] AudioClip rip;
    [Header("Callback")]
    [SerializeField]
    private UnityEvent OnSuccess;
    [SerializeField]
    private UnityEvent OnFail;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CoachCollider>())
        {
            OnSuccess.Invoke();
            GetComponent<AudioSource>().PlayOneShot(rip);
            col.gameObject.transform.SetParent(transform);
            col.transform.DOLocalMove(new Vector3(-0.000235f, -0.00453f, 0), 0.25f);
        }
        else
        {
            OnFail.Invoke();
        }
    }
}
