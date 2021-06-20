using System;
using TMPro;
using UnityEngine;

public class SliderButton : MonoBehaviour
{
    [SerializeField] private float _intitialValue = 50;
    [SerializeField] private float _increment = 10;
    [SerializeField] private TextMeshProUGUI _text;

    public Action<float> OnValueUpdate;

    public void SetValue(float newValue)
    {
        _intitialValue = newValue;
        UpdateText();
    }
    
    public void UpButton()
    {
        _intitialValue += _increment;
        if (_intitialValue > 100) _intitialValue = 100f;
        UpdateText();
    }

    public void DownButton()
    {
        _intitialValue -= _increment;
        if (_intitialValue < 0) _intitialValue = 0f;
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = ((int) _intitialValue).ToString();
    }

    private void OnValidate()
    {
        if (!_text) _text = this.GetComponentInChildren<TextMeshProUGUI>();
    }
}
