using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera myCamera;
    //[SerializeField] PipeRotationWinCondition winCondition;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        RaycastHit2D hit = Physics2D.Raycast(myCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, layerMask);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.GetComponent<KeyScript>())
            {
                hit.transform.gameObject.GetComponent<KeyScript>().TriggerKey();
            }
        }
    }
}
