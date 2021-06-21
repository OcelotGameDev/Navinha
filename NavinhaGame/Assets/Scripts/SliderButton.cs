using System;
using TMPro;
using UnityEngine;

public class SliderButton : MonoBehaviour
{
    [SerializeField] private float _value = 50;
    [SerializeField] private float _increment = 10;
    [SerializeField] private TextMeshProUGUI _text;

    public Action<float> OnValueUpdate;

    public void SetValue(float newValue)
    {
        _value = newValue;
        UpdateText();
    }
    
    public void UpButton()
    {
        _value += _increment;
        if (_value > 100) _value = 100f;
        
        OnValueUpdate?.Invoke(_value / 100f);
        UpdateText();
    }

    public void DownButton()
    {
        _value -= _increment;
        if (_value < 0) _value = 0f;
        
        OnValueUpdate?.Invoke(_value / 100f);
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = ((int) _value).ToString();
    }

    private void OnValidate()
    {
        if (!_text) _text = this.GetComponentInChildren<TextMeshProUGUI>();
    }
}
