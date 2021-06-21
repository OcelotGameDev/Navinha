using System;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    private static bool _hasplayedOnce = false;

    [SerializeField] private PlayableDirector _director;

    private void OnEnable()
    {
        if (_hasplayedOnce)
        {
            _director.time = _director.duration;
        }
        else
        {
            _hasplayedOnce = true;
        }
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            _director.time = _director.duration;
        }
    }

    private void OnValidate()
    {
        if (!_director) _director = this.GetComponent<PlayableDirector>();
    }
}