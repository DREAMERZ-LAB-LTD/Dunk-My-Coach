using UnityEngine;
using UnityEngine.Events;

public class CuttableHingeJointManipulator : MonoBehaviour
{
    [System.Serializable]
    public class Anchor
    {
        public Vector2 anchor;
        public Vector2 connectedAnchor;

    }
    public Transform joint;
    public Anchor leftAnchor;
    public bool destroyBoth = false;

    [Header("Rotation Setup")]
    public Transform parent;
    public Vector3 offsetFromParent;
    public Vector3 targetRotation = new Vector3(0, 0, 90);
    public float rotationTime = 1;
 
    public UnityEvent OnCut;
    public void CutterResponce()
    {
        if(OnCut != null)
            OnCut.Invoke();
    }

    public void CopyFrom(CuttableHingeJointManipulator referenceData)
    {
        joint = referenceData.joint;
        leftAnchor = referenceData.leftAnchor;
        destroyBoth = referenceData.destroyBoth;
        parent = referenceData.parent;
        targetRotation = referenceData.targetRotation;
        rotationTime = referenceData.rotationTime;
        OnCut = referenceData.OnCut;
        if (parent != null)
        { 
            offsetFromParent = transform.position - parent.transform.position;
        }
    }
    private void Awake()
    {
        if(parent != null)
            offsetFromParent = transform.position - parent.transform.position;
    }

    private void Update()
    {
       // if(parent != null)
           // transform.position = offsetFromParent + parent.transform.position;
    }
}
