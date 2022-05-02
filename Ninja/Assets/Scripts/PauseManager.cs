using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPause => _isPause;

    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] PauseMenu _pauseMenu;
    [SerializeField] BodyPartsManager _bodyPartsManager;

    bool _isPause;

    void Awake()
    {
        ClosePauseMenu();
        _pauseMenu.SetBackgroundBlurInteractable(true);

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
        _pauseMenu.SetBackgroundBlurInteractable(false);
        RefreshBodyPartsScreen();
    }

    void OpenPauseMenu()
    {
        StopTime();
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.SetState(PauseMenu.State.Pause);
        RefreshBodyPartsScreen();
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

    private void RefreshBodyPartsScreen() 
    {
        foreach (BodyPart bodyPart in _bodyPartsManager.bodyParts) 
        {
            switch (bodyPart.part)
            {
                case BodyPart.Part.Head:
                    _pauseMenu.bodyPartsImages[0].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                case BodyPart.Part.Body:
                    _pauseMenu.bodyPartsImages[1].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                case BodyPart.Part.LeftHand:
                    _pauseMenu.bodyPartsImages[2].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                case BodyPart.Part.RightHand:
                    _pauseMenu.bodyPartsImages[3].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                case BodyPart.Part.LeftLeg:
                    _pauseMenu.bodyPartsImages[4].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                case BodyPart.Part.RightLeg:
                    _pauseMenu.bodyPartsImages[5].gameObject.SetActive(bodyPart.state != BodyPart.State.Lost);
                    break;
                default:
                    break;
            }
        }
    }
}
