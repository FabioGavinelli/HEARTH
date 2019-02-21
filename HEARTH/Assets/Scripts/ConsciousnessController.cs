using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsciousnessController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource conscSource;

    void Start()
    {
        conscSource = this.GetComponent<AudioSource>();
    }
    
    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex > audioClips.Length || audioClips.Length == 0) return;
        conscSource.clip = audioClips[clipIndex];
        conscSource.Play();
    }
}
