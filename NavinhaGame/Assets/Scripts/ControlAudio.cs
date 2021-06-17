using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAudio : MonoBehaviour
{
    private AudioSource audioSrc;

    [SerializeField] private float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = musicVolume;
    }
}
