using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPause => _isPause;

    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] PauseMenu _pauseMenu;

    bool _isPause;

    void Awake()
    {
        ClosePauseMenu();

        _endGameManager.OnTimerEnded += OpenEndGameMenu;
        _endGameManager.OnAllBodyPartsLost += OpenEndGameMenu;
    }

    void OnDestroy()
    {
        _endGameManager.OnTimerEnded -= OpenEndGameMenu;
        _endGameManager.OnAllBodyPartsLost -= OpenEndGameMenu;
    }

    void Update()
    {
        if (_endGameManager.isTimerEnded)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            ClosePauseMenu();
        }
    }

    void OpenEndGameMenu()
    {
        StopTime();
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.SetState(PauseMenu.State.EndGame);
    }

    void OpenPauseMenu()
    {
        StopTime();
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.SetState(PauseMenu.State.Pause);
        _isPause = true;
    }

    public void ClosePauseMenu()
    {
        if (_endGameManager.isTimerEnded)
            return;

        StartTime();
        _pauseMenu.gameObject.SetActive(false);
        _isPause = false;
    }

    void StopTime()
    {
        Time.timeScale = 0f;
    }

    void StartTime()
    {
        Time.timeScale = 1f;
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
