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
    bool threeTriggered = false;

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
        target.DOMove(new Vector3(-0.3f, -4.57f, 0), 0.5f);
        animator.SetTrigger("stage3 Dodge");
        Invoke("Stage3Idle", 0.5f);
        oneTriggered = true;

    }
    void Stage3Idle()
    {
        if (!twoTriggered)
            animator.SetTrigger("stage3 Idle");
    }
    public void MoveToThird()
    {
        if (!oneTriggered)
            return;

        GetComponent<AudioSource>().PlayOneShot(jumpSound);
        twoTriggered = true;
        target.DOMove(new Vector3(1.1f, -4.57f, 0), 0.5f);
        animator.SetTrigger("stage3 Dodge");
        Invoke("Stage3Idle2", 0.5f);
    }
    void Stage3Idle2()
    {
        if (twoTriggered && !threeTriggered)

            animator.SetTrigger("stage3 Idle");
    }

    //drops the last rock
    public void MoveToLast()
    {
        if (!twoTriggered)
            return;

        threeTriggered = true;
        animator.SetTrigger("stage3 After Fall");
        Invoke("NewMethod", 1f);
        Invoke("GameWin", 2);
    }

    private void NewMethod()
    {
        targetRock.DORotate(Vector3.zero, 1f);
    }

    void GameWin()
    {
        coach.LevelCompleted();
    }
}
