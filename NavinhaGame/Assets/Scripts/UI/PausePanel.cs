using DG.Tweening;
using UnityEngine;

public class PausePanel : AbstractPanel<Pause>
{
    private Tween _tween;

    protected override void Awake()
    {
        base.Awake();
        _tween = _panel.transform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.OutBack).SetUpdate(true);
        _tween.onRewind += () => _panel.SetActive(false);
    }

    protected override void HandleGameStateChanged(IState state)
    {
        if (state is Pause)
        {
            _panel.SetActive(true);
            _tween.Restart();
        }
        else
        {
            _tween.SmoothRewind();
        }
    }
}