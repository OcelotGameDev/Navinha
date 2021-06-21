using DG.Tweening;

public abstract class AbstractTweenPanel<T> : AbstractPanel<T> where T: IState
{
    private Tween _tween;

    protected override void Awake()
    {
        base.Awake();
        _tween = _panel.transform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.OutBack).SetUpdate(true).From(0);
        _tween.onRewind += () => _panel.SetActive(false);
    }

    protected override void HandleGameStateChanged(IState state)
    {
        if (state is T)
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