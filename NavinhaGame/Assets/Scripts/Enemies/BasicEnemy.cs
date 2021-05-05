using UnityEngine;

public class BasicEnemy : AEnemy
{
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private float _timeBetweenShots = 1f;
    private float _timer;

    protected override void OnEnable()
    {
        base.OnEnable();

        _timer = _timeBetweenShots;
    }
    
    protected override void Move()
    {
        this._rigidbody.velocity = this.transform.right * _moveSpeed;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            Shoot();
            _timer = _timeBetweenShots;
        }
    }

    private void Shoot()
    {
        var bullet = PoolingSystem.Instance.SpawnObject("EnemyBulllet", this.transform);
    }
}