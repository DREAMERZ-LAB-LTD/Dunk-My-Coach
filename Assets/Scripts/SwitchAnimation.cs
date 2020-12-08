using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CoachCollider coach;
    [SerializeField] Transform target;
    [SerializeField] Transform targetRock;
    [SerializeField] AudioClip jumpSound;

    bool oneTriggered = false;
    bool twoTriggered = false;

    public void SwitchTo(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void MoveTo(Vector3 pos)
    {
        target.DOMove(pos, 0.5f);
    }

  public void MoveToSecond()
    {
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
        target.DOMove(new Vector3(-0.3f, -4.57f, 0), 1.25f);
        animator.SetTrigger("stage3 Dodge");
        Invoke("Stage3Idle", 1.25f);
        oneTriggered = true;

    }
    void Stage3Idle()
    {
        animator.SetTrigger("stage3 Idle");
    }
    public void MoveToThird()
    {
        if (!oneTriggered)
            return;
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
        twoTriggered = true;
        target.DOMove(new Vector3(1.1f, -4.57f, 0), 1.25f);
        animator.SetTrigger("stage3 Dodge");
        Invoke("Stage3Idle", 1.25f);

    }
    public void MoveToLast()
    {
        if (!twoTriggered)
            return;
        animator.SetTrigger("stage3 After Fall");
        targetRock.DORotate(Vector3.zero, 0.3f);
        Invoke("GameWin", 2);
    }

    void GameWin()
    {
        coach.LevelCompleted();
    }
}
