using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPause => _isPause;

    [SerializeField] IntroManager _introManager;
    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] PauseMenu _pauseMenu;
    [SerializeField] BodyPartsManager _bodyPartsManager;

    bool _isPause;
    AudioSource music;

    void Awake()
    {
        ClosePauseMenu();
        _pauseMenu.SetBackgroundBlurInteractable(true);

        _introManager.OnEnded += OnSkipped;
        _endGameManager.OnTimerEnded += OpenEndGameMenu;
        _endGameManager.OnAllBodyPartsLost += OpenEndGameMenu;
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Start()
    {
        StopTime();
        _isPause = true;
    }

    void OnDestroy()
    {
        _introManager.OnEnded -= OnSkipped;
        _endGameManager.OnTimerEnded -= OpenEndGameMenu;
        _endGameManager.OnAllBodyPartsLost -= OpenEndGameMenu;
    }

    void Update()
    {
        if (!_introManager.ended)
            return;

        if (_endGameManager.isTimerEnded)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause)
        {
            music.Pause();
            _introManager.introWatched = false;
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            music.Play();
            _introManager.introWatched = true;
            ClosePauseMenu();
        }
    }

    void OnSkipped()
    {
        StartTime();
        _isPause = false;
    }

    void OpenEndGameMenu()
    {
        StopTime();
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.SetState(PauseMenu.State.EndGame);
        _pauseMenu.SetBackgroundBlurInteractable(false);
        _pauseMenu.SetCircleFill(_endGameManager.currentDuration / _endGameManager.duration);
        _pauseMenu.SetActivePauseText(false);

        if (_endGameManager.currentDuration < _endGameManager.duration)
        {
            _pauseMenu.SetCircleColor(Color.red);
            _pauseMenu.SetActiveBackToMenuButton("lose");
            _pauseMenu.SetActiveRestartButton(true);
        }
        else
        {
            _pauseMenu.SetActiveBackToMenuButton("win");
            _pauseMenu.SetActiveRestartButton(false);
        }

        RefreshBodyPartsScreen();
    }

    void OpenPauseMenu()
    {
        StopTime();
        _pauseMenu.gameObject.SetActive(true);
        _pauseMenu.SetState(PauseMenu.State.Pause);
        _pauseMenu.SetCircleFill(_endGameManager.currentDuration / _endGameManager.duration);
        _pauseMenu.SetActiveBackToMenuButton("pause");
        _pauseMenu.SetActivePauseText(true);
        _pauseMenu.SetActiveRestartButton(false);
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
        StartTime();
        SceneManager.LoadScene("MainScene");
    }

    public void RestartGame()
    {
        StartTime();
        SceneManager.LoadScene("GameplayScene");
    }

    private void RefreshBodyPartsScreen()
    {
        foreach (BodyPart bodyPart in _bodyPartsManager.bodyParts)
        {
            switch (bodyPart.part)
            {
                case BodyPart.Part.Head:
                    if(bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[0].sprite = _pauseMenu.headOff;
                    else
                        _pauseMenu.bodyPartsImages[0].sprite = _pauseMenu.headOn;
                    break;
                case BodyPart.Part.Body:
                    if (bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[1].sprite = _pauseMenu.torsoOff;
                    else
                        _pauseMenu.bodyPartsImages[1].sprite = _pauseMenu.torsoOn;
                    break;
                case BodyPart.Part.LeftHand:
                    if (bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[2].sprite = _pauseMenu.leftArmOff;
                    else
                        _pauseMenu.bodyPartsImages[2].sprite = _pauseMenu.leftArmOn;
                    break;
                case BodyPart.Part.RightHand:
                    if (bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[3].sprite = _pauseMenu.rightArmOff;
                    else
                        _pauseMenu.bodyPartsImages[3].sprite = _pauseMenu.rightArmOn;
                    break;
                case BodyPart.Part.LeftLeg:
                    if (bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[4].sprite = _pauseMenu.leftLegOff;
                    else
                        _pauseMenu.bodyPartsImages[4].sprite = _pauseMenu.leftLegOn;
                    break;
                case BodyPart.Part.RightLeg:
                    if (bodyPart.state == BodyPart.State.Lost)
                        _pauseMenu.bodyPartsImages[5].sprite = _pauseMenu.rightLegOff;
                    else
                        _pauseMenu.bodyPartsImages[5].sprite = _pauseMenu.rightLegOn;
                    break;
                default:
                    break;
            }
        }
    }
}
