using System;
using UnityEngine;

public class WinObserver : MonoBehaviour
{
    [SerializeField] private WinPanel _winPanel;
    public static bool GameEnded { get; private set; } = false;
    
    private void OnEnable()
    {
        Behaviour_Player.OnDie += PlayerDied;
        BossTwoMove.OnDie += BossDied;
        GameEnded = false;
    }

    private void OnDestroy()
    {
        Behaviour_Player.OnDie -= PlayerDied;
        BossTwoMove.OnDie -= BossDied;
    }

    private void PlayerDied()
    {
        _winPanel.SetText(false);
        GameEnded = true;
    }

    private void BossDied()
    {
        _winPanel.SetText(true);
        GameEnded = true;
    }

    private void OnValidate()
    {
        if (!_winPanel) _winPanel = this.GetComponent<WinPanel>();
    }
}