using System;
using UnityEngine;

public class BigEnemy : AEnemy
{
    public float currentHp, maxHp, speed;
    private Rigidbody2D rbody;
    private bool IsDead => currentHp <= 0;

    [SerializeField] private BigEnemyGun[] _guns;

    private void Awake()
    {
        _guns = this.GetComponentsInChildren<BigEnemyGun>();
    }

    void OnEnable()
    {
        currentHp = maxHp;
    }
    
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    protected override void Move()
    {
        rbody.velocity = rbody.transform.right * speed;
    }

    private void Die()
    {
        this.Despawn();
    }

    private void OnBecameVisible()
    {
        foreach (var gun in _guns)
        {
            gun.EnableShooting();
        }
    }

    private void OnBecameInvisible()
    {
        this.Despawn();
    }
}
