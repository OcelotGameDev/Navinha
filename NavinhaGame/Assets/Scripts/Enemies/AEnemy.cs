using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AEnemy : MonoBehaviour, IHittable
{
    [SerializeField] protected int _maxLifePoints = 3;
    private int _currentLifePoints = 0;

    private bool IsDead => _currentLifePoints <= 0;
    
    [SerializeField] protected Rigidbody2D _rigidbody;
    
    [FormerlySerializedAs("_invisibleSignal")] [SerializeField] private VisibleInvisibleSignals invisibleSignals;

    public static Action<GameObject> OnDespawn;

    protected virtual void OnEnable()
    {
        _currentLifePoints = _maxLifePoints;
        
        invisibleSignals.OnBecameInvisibleSignal += ListenBecameInvisible;
    }

    protected virtual void OnDisable()
    {
        invisibleSignals.OnBecameInvisibleSignal -= ListenBecameInvisible;
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
        Despawn();
    }

    private void OnBecameInvisible()
    {
        Despawn();
    }

    protected void Despawn()
    {
        OnDespawn?.Invoke(this.gameObject);
        this.gameObject.SetActive(false);
    }

    protected virtual void OnValidate()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Enemies") ;
        
        if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody2D>();

        if (invisibleSignals) return;
        invisibleSignals = this.GetComponentInChildren<VisibleInvisibleSignals>();

        if (invisibleSignals) return;
        invisibleSignals = this.GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<VisibleInvisibleSignals>();
    }
}