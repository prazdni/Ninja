using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    bool introWatched = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!introWatched)
            introWatched = GameObject.Find("PauseMenuManager").GetComponent<IntroManager>().introWatched;
    }

    public void PlayFootstepAudio()
    {
        if (introWatched)
        {
            audioSource.volume = 0.4f;
            audioSource.PlayOneShot(audioClipArray[Random.Range(0, audioClipArray.Length)]);
        }
    }
}
