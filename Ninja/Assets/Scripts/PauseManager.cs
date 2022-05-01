using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuPrefab;
    private bool isPause = false;

    private void Awake()
    {
        ClosePauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        StopTime();
        PauseMenuPrefab.SetActive(true);
        isPause = true;
    }

    public void ClosePauseMenu()
    {
        StartTime();
        PauseMenuPrefab.SetActive(false);
        isPause = false;
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }
    private void StartTime()
    {
        Time.timeScale = 1f;
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
