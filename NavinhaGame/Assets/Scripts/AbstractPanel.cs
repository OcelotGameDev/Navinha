using UnityEngine;


public abstract class AbstractPanel : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;

    protected virtual void Awake()
    {
        _panel.SetActive(false);
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    protected abstract void HandleGameStateChanged(IState state);
}
