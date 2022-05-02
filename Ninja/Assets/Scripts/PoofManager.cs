using System.Collections.Generic;
using UnityEngine;

public class PoofManager : MonoBehaviour
{
    [SerializeField] Poof _poofPrefab;

    List<Poof> _poofs;

    void Awake()
    {
        _poofs = new List<Poof>();
    }

    public void AddPoof(Vector3 position)
    {
        Poof poof = _poofs.Find(poof => !poof.gameObject.activeSelf);
        if (poof == null)
        {
            poof = Instantiate(_poofPrefab);
            _poofs.Add(poof);
            poof.SetPoofManager(this);
        }

        poof.transform.position = position;
        poof.gameObject.SetActive(true);
        poof.ShowPoof();
    }

    public void ReturnPoof(Poof poof)
    {
        poof.gameObject.SetActive(false);
    }
}
