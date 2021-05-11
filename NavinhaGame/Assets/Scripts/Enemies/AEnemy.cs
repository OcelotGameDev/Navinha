using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AEnemy : MonoBehaviour, IHittable
{
    [SerializeField] protected int _maxLifePoints = 3;
    private int _currentLifePoints = 0;

    private bool IsDead => _currentLifePoints <= 0;
    
    [SerializeField] protected Rigidbody2D _rigidbody;
    
    [SerializeField] private OnBecomeInvisibleSignal _invisibleSignal;

    protected virtual void OnEnable()
    {
        _currentLifePoints = _maxLifePoints;
        
        _invisibleSignal.OnInvisibleBecame += ListenBecameInvisible;
    }

    protected virtual void OnDisable()
    {
        _invisibleSignal.OnInvisibleBecame -= ListenBecameInvisible;
    }
    
    private void ListenBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    public void Hit(int damage = 1)
    {
        _currentLifePoints -= damage;

        if (IsDead)  Die();
    }

    private void Die()
    {
        this.gameObject.SetActive(false);   
    }

    protected virtual void OnValidate()
    {
        if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody2D>();
        
        if (_invisibleSignal) return;
        _invisibleSignal = this.GetComponentInChildren<OnBecomeInvisibleSignal>();

        if (_invisibleSignal) return;
        _invisibleSignal = this.GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<OnBecomeInvisibleSignal>();

    }
}