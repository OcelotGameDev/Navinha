using UnityEngine;

public class LargeEnemy : AEnemy
{
    [SerializeField] private float _moveSpeed = 1f;

    [SerializeField][Range(0f, 90f)] private float _shootingAngle = 45f;
    
    [SerializeField] private float _timeBetweenShots = 1f;
    private float _timer ;

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
        PoolingSystem.Instance.SpawnObject("EnemyBullet", this.transform).transform.Rotate(Vector3.forward, _shootingAngle);
        PoolingSystem.Instance.SpawnObject("EnemyBullet", this.transform).transform.Rotate(Vector3.forward, -_shootingAngle);
    }
}
