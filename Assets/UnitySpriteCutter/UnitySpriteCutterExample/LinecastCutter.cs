using UnityEngine;
using System.Collections.Generic;
using UnitySpriteCutter;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class LinecastCutter : MonoBehaviour
{
    public LayerMask layerMask;
    Vector2 mouseStart;
    [SerializeField] AudioClip cuttingSoundEffect;
    AudioSource audioSource;

    void Update()
    {
        Vector2 mouseEnd;

        if (Input.touchCount > 0)
        {
            var _currentPrimaryTouch = Input.GetTouch(0);

            //touch started
            if (_currentPrimaryTouch.phase == TouchPhase.Began)
                mouseStart = Camera.main.ScreenToWorldPoint(_currentPrimaryTouch.position);


            //touch ended
            else if (_currentPrimaryTouch.phase == TouchPhase.Ended)
            {
                mouseEnd = Camera.main.ScreenToWorldPoint(_currentPrimaryTouch.position);
                LinecastCut(mouseStart, mouseEnd, layerMask.value);
                GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
                GetComponent<AudioSource>().PlayOneShot(cuttingSoundEffect, Random.Range(0.5f, 1f));
            }
        }


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


        if (Input.GetMouseButtonUp(0))
        {
            mouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LinecastCut(mouseStart, mouseEnd, layerMask.value);
        }
#endif
    }

    void LinecastCut(Vector2 lineStart, Vector2 lineEnd, int layerMask = Physics2D.AllLayers)
    {
        List<GameObject> gameObjectsToCut = new List<GameObject>();
        RaycastHit2D[] hits = Physics2D.LinecastAll(lineStart, lineEnd, layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (HitCounts(hit))
            {
                gameObjectsToCut.Add(hit.transform.gameObject);
            }
        }
        if (gameObjectsToCut.Count > 0)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            GetComponent<AudioSource>().PlayOneShot(cuttingSoundEffect, Random.Range(0.5f, 1f));
        }

        foreach (GameObject go in gameObjectsToCut)
        {
            SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
            {
                lineStart = lineStart,
                lineEnd = lineEnd,
                gameObject = go,
                gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_INTO_TWO,
                //gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
            });



            if (output != null && output.secondSideGameObject != null)
            {
                Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
                if (output.firstSideGameObject.GetComponent<Rigidbody2D>())
                    newRigidbody.velocity = output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;

                HingeJoint2D hingeJoint2D = go.GetComponent<HingeJoint2D>();
                Destroy(go.GetComponent<HingeJoint2D>());
                Destroy(output.firstSideGameObject.GetComponent<HingeJoint2D>());
                Destroy(output.secondSideGameObject.GetComponent<HingeJoint2D>());

                StartCoroutine(NewMethod(go, output));
            }

            if (go.GetComponent<Rigidbody2D>())
                go.GetComponent<Rigidbody2D>().isKinematic = false;

            //go.SetActive(false);
        }
    }

    private IEnumerator NewMethod(GameObject go, SpriteCutterOutput output)
    {
        //yield return new WaitForSeconds(0.1f);
        yield return null;

        if (go.GetComponent<CuttableHingeJointManipulator>())
        {
            if (!go.GetComponent<CuttableHingeJointManipulator>().destroyBoth)
            {
                Vector3 pos = go.GetComponent<CuttableHingeJointManipulator>().joint.position;
                go.GetComponent<CuttableHingeJointManipulator>().joint.SetParent(null);
                go.GetComponent<CuttableHingeJointManipulator>().joint.position = pos;

                //Vector2 jointPosition = go.GetComponent<CuttableHingeJointManipulator>().joint.position;
                float jointPosition = go.GetComponent<CuttableHingeJointManipulator>().joint.position.x;

                float x = 0;
                for (var i = 0; i < output.firstSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length; i++)
                {
                    //x += output.firstSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices[i].x;
                    Vector3 worldPt = output.firstSideGameObject.transform.TransformPoint(output.firstSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices[i]);
                    //x += output.firstSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices[i].x;
                    x += worldPt.x;
                }
                float firstPosition = x / output.firstSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length;
                //Debug.Log(firstPosition);


                x = 0;
                for (var i = 0; i < output.secondSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length; i++)
                {
                    Vector3 worldPt = output.firstSideGameObject.transform.TransformPoint(output.secondSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices[i]);
                    x += worldPt.x;
                    //x += output.secondSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices[i].x;
                }
                float secondPosition = x / output.secondSideGameObject.GetComponent<MeshFilter>().sharedMesh.vertices.Length;
                //Debug.Log(secondPosition);


                float distance = Mathf.Abs(jointPosition - firstPosition);

                if (distance > Mathf.Abs(jointPosition - secondPosition))
                {

                    output.secondSideGameObject.AddComponent<HingeJoint2D>();
                    output.secondSideGameObject.GetComponent<HingeJoint2D>().anchor = go.GetComponent<CuttableHingeJointManipulator>().leftAnchor.anchor;

                    StartCoroutine(SecondUpdateHinge(output.secondSideGameObject, go.GetComponent<CuttableHingeJointManipulator>().leftAnchor.anchor));
                    StartCoroutine(Disappear(output.firstSideGameObject));

                    SetCuttableHingeJoint(go, output.secondSideGameObject);
                }
                else
                {
                    output.firstSideGameObject.AddComponent<HingeJoint2D>();
                    output.firstSideGameObject.GetComponent<HingeJoint2D>().anchor = go.GetComponent<CuttableHingeJointManipulator>().leftAnchor.anchor;
                    StartCoroutine(SecondUpdateHinge(output.firstSideGameObject, go.GetComponent<CuttableHingeJointManipulator>().leftAnchor.anchor));
                    StartCoroutine(Disappear(output.secondSideGameObject));

                    SetCuttableHingeJoint(go, output.firstSideGameObject);
                }
            }
            else
            {
                StartCoroutine(Disappear(output.firstSideGameObject));
                StartCoroutine(Disappear(output.secondSideGameObject));
            }
        }
        Destroy(go);//
    }

    private static void SetCuttableHingeJoint(GameObject go, GameObject newGO)
    {
        newGO.AddComponent<CuttableHingeJointManipulator>();
        newGO.GetComponent<CuttableHingeJointManipulator>().joint = go.GetComponent<CuttableHingeJointManipulator>().joint;
        newGO.GetComponent<CuttableHingeJointManipulator>().leftAnchor = go.GetComponent<CuttableHingeJointManipulator>().leftAnchor;
    }

    IEnumerator SecondUpdateHinge(GameObject mahJoint, Vector2 anchor)
    {
        yield return null;
        mahJoint.GetComponent<HingeJoint2D>().anchor = anchor;
    }

    IEnumerator Disappear(GameObject unimportant)
    {
        if (unimportant.GetComponent<SpriteRenderer>())
            unimportant.GetComponent<SpriteRenderer>().DOFade(0, 1f);

        if (unimportant.GetComponent<MeshRenderer>())
            unimportant.GetComponent<MeshRenderer>().material.DOFade(0, 1f);

        //yield return new WaitForSeconds(0.1f);

        unimportant.layer = LayerMask.NameToLayer("NoCollision");
        yield return new WaitForSeconds(1f);
        Destroy(unimportant);
    }



    bool HitCounts(RaycastHit2D hit)
    {
        return (hit.transform.GetComponent<SpriteRenderer>() != null ||
                 hit.transform.GetComponent<MeshRenderer>() != null);
    }

}
