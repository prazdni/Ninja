using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    float currentTime = 999f;
    bool timerActive = false;
    public Canvas canvas;
    float currentAlpha = 1f;
    float desiredAlpha = 0f;


    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource ambienceAudioSource;

    private void Update()
    {
        if (timerActive)
        {
            currentTime -= 0.1f;
            musicAudioSource.volume -= 0.001f;
            ambienceAudioSource.volume -= 0.0001f;

            currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 0.42f * Time.deltaTime);
            canvasGroup.alpha = currentAlpha;
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
