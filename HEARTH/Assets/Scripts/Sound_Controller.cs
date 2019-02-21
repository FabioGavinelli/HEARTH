using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Controller : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioClip[] audioClips;

    public int getAudioSourceLenght()
    {
        return audioSources.Length;
    }

    public void SetVolume(int sourceIndex, float vol)
    {
        if (sourceIndex > audioSources.Length || audioSources.Length == 0) return;

        audioSources[sourceIndex].volume = vol;
    }

    public void StopAudioSource(int sourceIndex)
    {
        if (sourceIndex > audioSources.Length || audioSources.Length == 0) return;
        audioSources[sourceIndex].Stop();
    }

    public void PlayAudioSource(int sourceIndex, int clipIndex)
    {
        if (clipIndex > audioClips.Length || sourceIndex > audioSources.Length || audioSources.Length == 0 || audioClips.Length == 0) return;

        if (clipIndex >= 0)
        {
            audioSources[sourceIndex].clip = audioClips[clipIndex];
        }
        audioSources[sourceIndex].Play();

    }

    public IEnumerator volumeUp(int sourceIndex, float maxVolume, float time, bool forceStart)
    {
        if (maxVolume > 1 && maxVolume <= 0) yield break;
        if (sourceIndex > audioSources.Length || audioSources.Length == 0) yield break;
        if (forceStart) audioSources[sourceIndex].Play();

        while (audioSources[sourceIndex].volume < maxVolume)
        {
            audioSources[sourceIndex].volume += Time.deltaTime / (time / maxVolume);
            yield return null;
        }
    }

    public IEnumerator volumeDown(int sourceIndex, float minVolume, float time, bool forceStop)
    {
        if (minVolume < 0 && minVolume > 1) yield break;
        if (sourceIndex > audioSources.Length || audioSources.Length == 0) yield break;

        while (audioSources[sourceIndex].volume > minVolume)
        {
            audioSources[sourceIndex].volume -= Time.deltaTime / (time);
            yield return null;
        }

        if (forceStop) audioSources[sourceIndex].Stop();
    }

    /*
    public IEnumerator volumeUp(AudioSource audio, float maxVolume, float time, bool forceStart)
    {
        if (maxVolume > 1 && maxVolume <= 0) yield break;
        if (forceStart) audio.Play();

        while (audio.volume < maxVolume)
        {
            audio.volume += Time.deltaTime / (time / maxVolume);
            yield return null;
        }
    }
    

    public IEnumerator volumeDown(AudioSource audio, float minVolume, float time, bool forceStop)
    {
        if (minVolume < 0 && minVolume > 1) yield break;

        while (audio.volume > minVolume)
        {
            audio.volume -= Time.deltaTime / (time);
            yield return null;
        }

        if (forceStop) audio.Stop();
    }
    */

}
