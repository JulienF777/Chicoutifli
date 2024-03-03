using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //On start , play the first audio clip
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //If the audio clip has finished playing, play in loop
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }
}
