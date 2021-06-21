using TMPro;
using UnityEngine;

public class WinPanel : AbstractTweenPanel<WinState>
{
    [TextArea][SerializeField] private string _winText;
    [TextArea][SerializeField] private string _loseText;
    [SerializeField] private TextMeshProUGUI _text;
    
    public void SetText(bool isWinText)
    {
        _text.text = isWinText ? _winText : _loseText;
    }
}