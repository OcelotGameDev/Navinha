using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public abstract class AbstractPanel<T> : MonoBehaviour where T : IState
{
    [SerializeField] protected GameObject _panel;

    protected virtual void Awake()
    {
        _panel.SetActive(false);
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    protected virtual void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    protected virtual void HandleGameStateChanged(IState state)
    {
        _panel.SetActive(state is T);
    }

    protected virtual void OnValidate()
    {
        if (!_panel) _panel = this.GetComponentInChildren<Image>()?.gameObject;
    }
}
