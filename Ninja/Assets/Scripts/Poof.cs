using System.Collections;
using UnityEngine;

public class Poof : MonoBehaviour
{
    [SerializeField] Animation _animation;
    PoofManager _poofManager;

    public void SetPoofManager(PoofManager poofManager)
    {
        _poofManager = poofManager;
    }

    public void ShowPoof()
    {
        StartCoroutine(ShowPoofInternal());
    }

    IEnumerator ShowPoofInternal()
    {
        _animation.Play();

        yield return new WaitWhile(() => _animation.isPlaying);

        _poofManager.ReturnPoof(this);
    }
}
