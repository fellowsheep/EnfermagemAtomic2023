using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audios")]
public class AudioScript : AudioEvent
{
    public AudioClip clip;

    [Range(0.5f,1.5f)]
    public float volume;

    public override void Play(AudioSource source)
    {
        if(clip == null)
        {
            return;
        }

        source.clip = clip;
        source.volume = Random.Range(0.5f, volume);
        source.Play();
    }
}
