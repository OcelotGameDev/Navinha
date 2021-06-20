using DG.Tweening;
using UnityEngine;

public class OptionsPanel : AbstractPanel<Options>
{
    private Tween _tween;
    private IState _lastState;

    [SerializeField] private SliderButton _slider;
    
    private float _currentVolume = 0;
    private FMOD.Studio.Bus Master;
    
    protected override void Awake()
    {
        base.Awake();
        
        _tween = _panel.transform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.OutBack).SetUpdate(true);
        _tween.onRewind += () => _panel.SetActive(false);
    }

    protected override void HandleGameStateChanged(IState state)
    {
        // _panel.SetActive(state is Options);

        if (state is Options)
        {
            LoadOptions();
            _panel.SetActive(true);
            _tween.Restart();
        }
        else 
        {
            if (_lastState is Options)
            {
                SaveOptions();
            }
            
            _tween.SmoothRewind();
        }

        _lastState = state;
    }

    private void SaveOptions()
    {
        
    }

    private void LoadOptions()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            
        }
    }

    private void UpdateVolumeValue(float newValue)
    {
        
    }
}
