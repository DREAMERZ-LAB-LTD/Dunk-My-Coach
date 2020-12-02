using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CoachCollider : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool wet = false;
    float delay = 2;
    public bool won = false;
    [SerializeField] Transform levelCompletionUI;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject collisionParticle;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(RandomColliderPosition());
    //}

    //IEnumerator RandomColliderPosition()
    //{
    //    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
    //    Vector2 offset = GetComponent<CapsuleCollider2D>().offset;
    //    offset.x = Random.Range(-0.2f, 0.2f);
    //    GetComponent<CapsuleCollider2D>().offset = offset;
    //    StartCoroutine(RandomColliderPosition());
    //}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col) { 
        GameObject c = Instantiate(collisionParticle, col.transform.position, Quaternion.identity);
        c.GetComponent<ParticleSystem>().Play();
            }
        if (wet)
            return;

        particle.Play();

        if (col.transform.CompareTag("Metaball_liquid"))
        {
            wet = true;
            Debug.Log("wet");
            animator.SetTrigger("Wet");
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
                if (!won)
                {
                    animator.SetTrigger("After fall");
                    won = true;
                    levelCompletionUI.gameObject.SetActive(true);
                    levelCompletionUI.localScale = Vector3.zero;
                    levelCompletionUI.DOScale(1, 1);
                    //Invoke("LoadNextScene", 4);
                }
            }
        }
    }
    //void LoadNextScene()
    //{
    //    SceneManager.LoadScene(nextSceneName);
    //}
}
