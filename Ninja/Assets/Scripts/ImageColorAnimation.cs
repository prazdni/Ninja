using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorAnimation : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] Image _targetImage;
    Color lerpedColor;

    private void Update()
    {
        lerpedColor = Color.Lerp(startColor, endColor, _endGameManager.currentDuration / _endGameManager.duration);
        lerpedColor.a = 1f;
        _targetImage.color = lerpedColor;
    }
}
