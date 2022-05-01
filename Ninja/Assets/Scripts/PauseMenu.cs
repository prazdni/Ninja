using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public enum State
    {
        Pause,
        EndGame
    }

    [SerializeField] TMP_Text _pauseText;
    [SerializeField] TMP_Text _gameOverText;

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
}
