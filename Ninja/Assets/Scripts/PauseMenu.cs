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

    [SerializeField] Button _backgroundBlurButton;
    [SerializeField] Image _circle;
    [SerializeField] Image _pauseText;

    public Sprite headOn;
    public Sprite headOff;
    public Sprite leftArmOn;
    public Sprite leftArmOff;
    public Sprite rightArmOn;
    public Sprite rightArmOff;
    public Sprite torsoOn;
    public Sprite torsoOff;
    public Sprite leftLegOn;
    public Sprite leftLegOff;
    public Sprite rightLegOn;
    public Sprite rightLegOff;

    [SerializeField] Button pauseBackToMenuButton;
    [SerializeField] Button winScreenBackToMenuButton;
    [SerializeField] Button looseScreenBackToMenuButton;
    [SerializeField] Button _soundButton;
    [SerializeField] Button _restartButton;

    public void SetState(State state)
    {
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

    public void SetCircleFill(float fillAmount) 
    {
        _circle.fillAmount = fillAmount;
    }

    public void SetCircleColor(Color color) 
    {
        _circle.color = color;
    }

    public void SetActiveBackToMenuButton(string buttonName) 
    {
        switch (buttonName)
        {
            case "pause":
                pauseBackToMenuButton.gameObject.SetActive(true);
                winScreenBackToMenuButton.gameObject.SetActive(false);
                looseScreenBackToMenuButton.gameObject.SetActive(false);
                break;
            case "win":
                pauseBackToMenuButton.gameObject.SetActive(false);
                winScreenBackToMenuButton.gameObject.SetActive(true);
                looseScreenBackToMenuButton.gameObject.SetActive(false);
                break;
            case "lose":
                pauseBackToMenuButton.gameObject.SetActive(false);
                winScreenBackToMenuButton.gameObject.SetActive(false);
                looseScreenBackToMenuButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetActivePauseText(bool isActive) 
    {
        _pauseText.gameObject.SetActive(isActive);
    }

    public void SetActiveSoundButton(bool isActive)
    {
        _soundButton.gameObject.SetActive(isActive);
    }

    public void SetActiveRestartButton(bool isActive)
    {
        _restartButton.gameObject.SetActive(isActive);
    }
}
