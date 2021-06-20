using System;
using TMPro;
using UnityEngine;

public class SliderButton : MonoBehaviour
{
    [SerializeField] private float _volumeIncrement = 10;
    [SerializeField] private TextMeshProUGUI _text;
    private float _volume = 50;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            _volume = 
        }
    }

    public void UpButton()
    {
        _volume += _volumeIncrement;
        if (_volume > 100) _volume = 100f;
        UpdateText();
    }

    public void DownButton()
    {
        _volume -= _volumeIncrement;
        if (_volume < 0) _volume = 0f;
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = ((int) _volume).ToString();
    }

    private void OnValidate()
    {
        if (!_text) _text = this.GetComponentInChildren<TextMeshProUGUI>();
    }
}
