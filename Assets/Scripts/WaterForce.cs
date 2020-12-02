using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterForce : MonoBehaviour
{
    public float thurst;
    WaitForSeconds one = new WaitForSeconds(1);
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(TriggerOnOff());
    }


    IEnumerator TriggerOnOff()
    {
        boxCollider2D.enabled = false;
        yield return null;
        boxCollider2D.enabled = true;
        yield return null;
        StartCoroutine(TriggerOnOff());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thurst);
        }
    }

}
