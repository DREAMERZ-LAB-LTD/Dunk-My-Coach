using UnityEngine;
using DG.Tweening;

public class RandomMovementAnimation : MonoBehaviour
{
    public Vector3 min;
    public Vector3 max;
    public float timeMin;
    public float timeMax;

    void Start()
    {
        transform.DOLocalMove(new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z)), Random.Range(timeMin, timeMax)).SetLoops(-1, LoopType.Restart);
    }
}
