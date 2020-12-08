using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class HammerTrigger : MonoBehaviour
{
    [SerializeField] SpriteRenderer iceBar;
    [SerializeField] Transform hammer;
    [SerializeField] Rigidbody2D rock;
    [SerializeField] float hitTime;
    [SerializeField] Vector3 hitRotation;
    [SerializeField] float pushbackTime;
    [SerializeField] Vector3 pushbackRotation;
    [SerializeField] float idleTime;
    [SerializeField] Vector3 idleRotation;
    [SerializeField] UnityEvent callAfterCompletion;
    bool triggeredAlready = false;
    [SerializeField] AudioClip iceBreak;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggeredAlready)
            return;

        if (other.gameObject.CompareTag("Metaball_liquid"))
        {
            triggeredAlready = true;
            Debug.Log("Hammer triggered");
            ChainReaction();
        }
    }
    [ContextMenu("test")]
    void ChainReaction()
    {
        StartCoroutine(HammerDropRoutine());
    }

    IEnumerator HammerDropRoutine()
    {
        hammer.rotation = Quaternion.identity;
        yield return null;
        hammer.DORotate(hitRotation, hitTime, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(hitTime);
        iceBar.DOFade(0, 1);
        GetComponent<AudioSource>().PlayOneShot(iceBreak);
        if (iceBar.gameObject.GetComponent<Rigidbody2D>())
            iceBar.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        hammer.DORotate(pushbackRotation, pushbackTime);
        callAfterCompletion.Invoke();
        yield return new WaitForSeconds(pushbackTime);
        rock.isKinematic = false;
        hammer.DORotate(idleRotation, idleTime);
        yield return new WaitForSeconds(idleTime);
    }
}
