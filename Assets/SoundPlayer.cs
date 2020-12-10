using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    public void PlayAudioClips()
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClips[i]);
        }
    }
}
