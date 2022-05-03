using System;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public Action OnEnded;

    public bool ended => _skipped;

    [SerializeField] GameObject _canvas;
    bool _skipped;

    AudioSource music;
    AudioSource ambience;
    AudioSource introMusic;
    public bool introWatched = false;
    private void Start()
    {
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        ambience = GameObject.Find("Ambience").GetComponent<AudioSource>();
        introMusic = GameObject.Find("Intro Music").GetComponent<AudioSource>();
        introMusic.Play();
    }
    void Update()
    {
        if (_skipped)
        {
            introMusic.Stop();
            introWatched = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            introMusic.Stop();
            introWatched = true;
            _skipped = true;
            _canvas.SetActive(false);
            PlayGameAudio();
            OnEnded?.Invoke();
        }
    }

    void PlayGameAudio ()
    {
        music.GetComponent<AudioSource>().Play();
        ambience.GetComponent<AudioSource>().Play();
    }
}
