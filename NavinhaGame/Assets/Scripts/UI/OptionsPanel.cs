using System;
using DG.Tweening;
using UnityEngine;

public class OptionsPanel : AbstractPanel<Options>
{
    private Tween _tween;
    private IState _lastState;

    [SerializeField] private SliderButton _slider;
    
    private static float _currentVolume = 0.4f;
    private FMOD.Studio.Bus Master;
    
    protected override void Awake()
    {
        base.Awake();

        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        LoadOptions();
        SetCurrentVolume();

        _tween = _panel.transform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.OutBack).SetUpdate(true).From(0);
        _tween.onRewind += () => _panel.SetActive(false);

        _slider.OnValueUpdate += UpdateVolumeValue;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _slider.OnValueUpdate -= UpdateVolumeValue;
    }

    protected override void HandleGameStateChanged(IState state)
    {
        // _panel.SetActive(state is Options);

        if (state is Options)
        {
            LoadOptions();
            _tween.Restart();
            _panel.SetActive(true);
            SetVolumeSlider();
        }
        else if (_lastState is Options)
        {
            SaveOptions();
            _tween.SmoothRewind();
        }
        
        _lastState = state;
    }

    private static void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", _currentVolume);
        PlayerPrefs.Save();
    }

    private static void LoadOptions()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            _currentVolume = PlayerPrefs.GetFloat("Volume");
        }
    }

    private void SetCurrentVolume()
    {
        Master.setVolume(_currentVolume);
    }

    private void UpdateVolumeValue(float newValue)
    {
        _currentVolume = newValue;
        SetCurrentVolume();
    }

    private void SetVolumeSlider()
    {
        _slider.SetValue(_currentVolume * 100);
    }
}
