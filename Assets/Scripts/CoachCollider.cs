using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CoachCollider : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string animTriggerIdle = "stage1 Idle";
    [SerializeField] string animTriggerWet = "wet";
    [SerializeField] string animTriggerAfterFall = "After fall";
    bool wet = false;
    float delay = 1.5f;
    public bool won = false;
    [SerializeField] Transform levelCompletionUI;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject collisionParticle;
    [SerializeField] Material postWinCoachMaterial;
    [SerializeField] SkinnedMeshRenderer coachRenderer;
    [SerializeField] bool winWithWater = true;

    [SerializeField] List<AudioClip> onceImmediateAfterWet = new List<AudioClip>();
    [SerializeField] List<AudioClip> wetSoundOnce = new List<AudioClip>();

    void Start()
    {
        animator.SetTrigger(animTriggerIdle);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col)
        {
            GameObject c = Instantiate(collisionParticle, col.transform.position, Quaternion.identity);
            c.GetComponent<ParticleSystem>().Play();
        }
        if (wet)
            return;

        if (particle) particle.Play();
        Invoke("ChangeMaterial", 0.5f);

        if (col.transform.CompareTag("Metaball_liquid") && winWithWater)
        {
            wet = true;
            animator.SetTrigger(animTriggerWet);
        }
    }

    private void Update()
    {
        if (wet)
        {
            if (delay > 0)
                delay -= Time.deltaTime;
            else
            {
                LevelCompletedAnimation();

            }
        }
    }

    public void LevelCompletedAnimation()
    {
        if (!won)
        {
            won = true;
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().Stop();
                for (int i = 0; i < onceImmediateAfterWet.Count; i++)
                {
                    GetComponent<AudioSource>().PlayOneShot(onceImmediateAfterWet[i]);
                }
            }

            Debug.Log("called ");
            if (!string.IsNullOrEmpty(animTriggerAfterFall))
                animator.SetTrigger(animTriggerAfterFall);

            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Stop();
            for (int i = 0; i < wetSoundOnce.Count; i++)
            {
                GetComponent<AudioSource>().PlayOneShot(wetSoundOnce[i]);
            }
        }

        levelCompletionUI.gameObject.SetActive(true);
        levelCompletionUI.localScale = Vector3.zero;
        levelCompletionUI.DOScale(1, 1);
    }

    void ChangeMaterial()
    {
        if (postWinCoachMaterial != null)
            coachRenderer.material = postWinCoachMaterial;
    }
}
