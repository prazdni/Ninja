using System.Collections;
using UnityEngine;

public class CustomPufAnimation : MonoBehaviour
{
    public bool isPlaying => _isPlaying;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] float _between;

    bool _isPlaying;

    public void Play()
    {
        StartCoroutine(ShowPufInternal());
    }

    IEnumerator ShowPufInternal()
    {
        _isPlaying = true;

        foreach (Sprite sprite in _sprites)
        {
            _spriteRenderer.sprite = sprite;
            yield return new WaitForSecondsRealtime(_between);
        }

        _isPlaying = false;
    }
}
