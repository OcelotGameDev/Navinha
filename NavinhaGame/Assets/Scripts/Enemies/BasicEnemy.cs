using System;
using UnityEngine;

public class BasicEnemy : AEnemy
{
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private float _timeBetweenShots = 1f;
    private float _timer;
    public string explosion;

    private bool _canShoot = false;
    
    private VisibleInvisibleSignals _invisibleSignals;

    [SerializeField] private Transform _gunpoint;

    private void Awake()
    {
        _invisibleSignals = this.GetComponentInChildren<VisibleInvisibleSignals>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _canShoot = false;

        _timer = _timeBetweenShots;

        _invisibleSignals.OnBecameInvisibleSignal += Despawn;
        _invisibleSignals.OnBecameVisibleSignal += EnableShooting;
    }

    protected override void OnDisable()
    {
        SpawnVFX();
        base.OnDisable();
        _invisibleSignals.OnBecameInvisibleSignal -= Despawn;
        _invisibleSignals.OnBecameVisibleSignal -= EnableShooting;
    }
    
    protected override void Move()
    {
        this._rigidbody.velocity = this.transform.right * _moveSpeed;
    }

    private void Update()
    {
        if (!_canShoot) return;
        
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            Shoot();
            _timer = _timeBetweenShots;
        }
    }

    void SpawnVFX()
    {
        GameObject vfx = PoolingSystem.Instance.SpawnObject(explosion);
        if (vfx != null)
        {
            vfx.transform.position = this.transform.position;
            vfx.SetActive(true);
        }
    }

    private void Shoot()
    {
        var bullet = PoolingSystem.Instance.SpawnObject("BossBullet", this.transform);
        bullet.transform.rotation = _gunpoint.rotation;
    }
    
    private void EnableShooting()
    {
        Debug.Log($"{this.gameObject} Shooting Enabled");
        _canShoot = true;
    }
}