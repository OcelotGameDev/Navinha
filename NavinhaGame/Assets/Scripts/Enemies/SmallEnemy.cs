using System.Collections;
using UnityEngine;

public class SmallEnemy : AEnemy
{
    [SerializeField] private float _movingDistance = 5f;
    private Vector3 _targetDistance;
    
    [SerializeField] private float _timeToWait = 5f;
    private float _timer;

    private static GameObject _player = null;
    
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(MovementAwait());
        
        if (_player != null) return;
        
        var behaviourPlayer = FindObjectOfType<Behaviour_Player>();
        if (!behaviourPlayer) return;
        
        _player = behaviourPlayer.gameObject;
    }

    private IEnumerator MovementAwait()
    {
        yield return new WaitForSeconds(0.02f);

        _targetDistance = this.transform.forward * _movingDistance;
        
        
    }

    protected override void Move()
    {
           
    }
}
