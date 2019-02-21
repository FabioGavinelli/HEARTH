using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsciousnessController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    private bool[] reproduced;
    private AudioSource conscSource;

    void Start()
    {
        reproduced = new bool[audioClips.Length];
        conscSource = this.gameObject.GetComponent<AudioSource>();
        for(int i = 0; i < audioClips.Length; i++)
        {
            reproduced[i] = false;
        }
        
    }
    
    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex > audioClips.Length || audioClips.Length == 0 || reproduced[clipIndex] == true) return;
        conscSource.clip = audioClips[clipIndex];
        conscSource.Play();
        reproduced[clipIndex] = true;
    }
}
