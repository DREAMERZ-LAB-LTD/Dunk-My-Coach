using UnityEngine;

public class CuttableHingeJointManipulator : MonoBehaviour
{
    public Transform joint;
    public Anchor leftAnchor;
    public bool destroyBoth = false;

    [System.Serializable]
    public class Anchor
    {
        public Vector2 anchor;
        public Vector2 connectedAnchor;
    }
}
