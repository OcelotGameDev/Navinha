using System;
using UnityEngine;

public class VisibleInvisibleSignals : MonoBehaviour
{
    public Action OnBecameInvisibleSignal;
    public Action OnBecameVisibleSignal;
    
    private void OnBecameInvisible()
    {
        OnBecameInvisibleSignal?.Invoke();   
    }

    private void OnBecameVisible()
    {
        OnBecameVisibleSignal?.Invoke();
    }
}