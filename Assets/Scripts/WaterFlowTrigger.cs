using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaterFlowTrigger : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public List<KeyScript> keys = new List<KeyScript>();

    public void KeyOpened()
    {
        bool alreadydone = true;
        for (int i = 0; i < keys.Count; i++)
        {
            if (!keys[i].alreadydone) alreadydone = false;
        }

        if (alreadydone)
            GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
