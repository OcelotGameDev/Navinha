using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AEnemy : MonoBehaviour, IHittable
{
    [SerializeField] protected int _maxLifePoints = 3;
    private int _currentLifePoints = 0;

    private bool IsDead => _currentLifePoints <= 0;
    
    [SerializeField] protected Rigidbody2D _rigidbody;

    protected virtual void OnEnable()
    {
        _currentLifePoints = _maxLifePoints;
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

    protected void OnValidate()
    {
        if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody2D>();
    }
}