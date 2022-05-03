using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorAnimation : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] SpriteRenderer _targetSpriteRenderer;
    Color lerpedColor;

    private void Update()
    {
        lerpedColor = Color.Lerp(startColor, endColor, _endGameManager.currentDuration/_endGameManager.duration);
        lerpedColor.a = 1f;
        _targetSpriteRenderer.color = lerpedColor;
    }
}
