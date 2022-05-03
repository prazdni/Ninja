using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    float currentTime = 999f;
    bool timerActive = false;
    public AudioSource music;
    public AudioSource ambience;
    public Canvas canvas;
    float currentAlpha = 1f;
    float desiredAlpha = 0f;

    private void Start()
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        ambience = GameObject.Find("Ambience").GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (timerActive)
        {
            currentTime -= 0.1f;
            music.volume -= 0.001f;
            ambience.volume -= 0.0001f;

            currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 0.42f * Time.deltaTime);
            canvas.GetComponent<CanvasGroup>().alpha = currentAlpha;
        }
        if (currentTime <= 0)
        {
            timerActive = false;
            SceneManager.LoadScene("GameplayScene");
        }
    }
    public void OpenGameplayScene()
    {
        currentTime = 100.0f;
        timerActive = true;
    }
}
