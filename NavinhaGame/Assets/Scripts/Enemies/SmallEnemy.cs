using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SmallEnemy : AEnemy
{
    [SerializeField] private float _movingDistance = 5f;
    private Vector3 _targetDistance;

    [SerializeField] private float _shootingSpeed = 5f;

    [SerializeField] private float _timeToWait = 5f;

    private static GameObject _player = null;
    public GameObject vfx;

    private bool _lookAt = false;
    private bool _shoot = false;

    private VisibleInvisibleSignals _invisibleSignals;
    
    private void Awake()
    {
        _invisibleSignals = this.GetComponentInChildren<VisibleInvisibleSignals>();
    }

    protected override void OnEnable()
    {
        _lookAt = false;
        _shoot = false;

        base.OnEnable();

        StartCoroutine(MovementAwait());

        if (_player != null) return;

        var behaviourPlayer = FindObjectOfType<Behaviour_Player>();
        if (!behaviourPlayer) return;

        _player = behaviourPlayer.gameObject;
        
        _invisibleSignals.OnBecameInvisibleSignal += Despawn;
    }

    protected override void OnDisable()
    {
        Instantiate(vfx, this.transform.position, Quaternion.identity);
        base.OnDisable();
        _invisibleSignals.OnBecameInvisibleSignal -= Despawn;
    }

    private IEnumerator MovementAwait()
    {
        yield return new WaitForSeconds(0.02f);

        _targetDistance = this.transform.position + ( this.transform.right * _movingDistance);

        this.transform.DOMove(_targetDistance, 1f).onComplete += () => _lookAt = true;

        yield return new WaitForSeconds(_timeToWait);
        _lookAt = false;
        _shoot = true;
    }

    protected override void Move()
    {
        if (_player == null) return;

        if (_lookAt)
        {
            var aux = _player.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aux.y, aux.x));
        }

        if (_shoot)
        {
            _rigidbody.velocity = this.transform.right * _shootingSpeed;
        }
    }
}