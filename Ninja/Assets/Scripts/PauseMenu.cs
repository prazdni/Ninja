using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public enum State
    {
        Pause,
        EndGame
    }

    public List<Image> bodyPartsImages;

    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] TMP_Text _pauseText;
    [SerializeField] Button _backgroundBlurButton;

    public void SetState(State state)
    {
        _pauseText.gameObject.SetActive(state == State.Pause);
        _gameOverText.gameObject.SetActive(state == State.EndGame);

        switch (state)
        {
            case State.Pause:
                break;
            case State.EndGame:
                break;
        }
    }

    public void SetBackgroundBlurInteractable(bool isInteracrable) 
    {
        _backgroundBlurButton.interactable = isInteracrable;
    }
}
