using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepAudio()
    {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioClipArray[Random.Range(0, audioClipArray.Length)]);
    }
}
