using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class KeyScript : MonoBehaviour
{
    public bool usable = true;
    public Sprite usableSprite;


    [SerializeField] Transform model;
    [SerializeField] List<WaterForce> waterForces = new List<WaterForce>();
    [SerializeField] List<WaterForce> waterForcesSecond = new List<WaterForce>();
    [SerializeField] GameObject disableWhenDone;
    [SerializeField] Vector3 endPosition;
    [SerializeField] AudioClip clip;
    [SerializeField] WaterFlowTrigger waterFlowTrigger;
    [HideInInspector] public bool alreadydone = false;
    public bool onlyOnce = true;
    int a = 0;

    public void TriggerKey()
    {
        if (!usable)
            return;

        if (alreadydone && onlyOnce) return;

        if (model.localPosition == endPosition)
            model.DOLocalMove(Vector3.zero, 0.25f);
        else
            model.DOLocalMove(endPosition, 0.25f);

        if (disableWhenDone) disableWhenDone.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(clip);
        alreadydone = true;
        if (waterFlowTrigger) waterFlowTrigger.KeyOpened();


        if (a == 0)
        {
            if (waterForces.Count > 0)
            {
                for (int i = 0; i < waterForces.Count; i++)
                {
                    waterForces[i].gameObject.SetActive(true);
                }
            }
            if (waterForces.Count > 0)
            {
                for (int i = 0; i < waterForcesSecond.Count; i++)
                {
                    waterForcesSecond[i].gameObject.SetActive(false);
                }
            }
            a = 1;
        }
        else
        {
            a = 0;

            if (waterForces.Count > 0)
            {
                for (int i = 0; i < waterForcesSecond.Count; i++)
                {
                    waterForcesSecond[i].gameObject.SetActive(true);
                }
            }
            if (waterForces.Count > 0)
            {
                for (int i = 0; i < waterForces.Count; i++)
                {
                    waterForces[i].gameObject.SetActive(false);
                }
            }
        }
    }


    public void Usable()
    {
        usable = true;
        model.GetComponent<SpriteRenderer>().sprite = usableSprite;
    }
}
