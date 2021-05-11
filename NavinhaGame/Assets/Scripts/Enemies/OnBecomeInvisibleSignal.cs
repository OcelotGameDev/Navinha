using System;
using UnityEngine;

public class OnBecomeInvisibleSignal : MonoBehaviour
{
    public Action OnInvisibleBecame;
    
    private void OnBecameInvisible()
    {
        OnInvisibleBecame?.Invoke();   
    }
}