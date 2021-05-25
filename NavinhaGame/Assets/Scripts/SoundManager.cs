using FMODUnity;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public StudioEventEmitter EventEmitter;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void OnValidate()
    {
        if (!EventEmitter) EventEmitter = this.GetComponent<StudioEventEmitter>();
    }
}
