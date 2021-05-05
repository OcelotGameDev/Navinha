using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyBullet : MonoBehaviour, IHittable
{
    [SerializeField] private float _speed = 5;
    
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Update()
    {
        _rigidbody.velocity = this.transform.right * 5;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hittable = other.gameObject.GetComponent<IHittable>();
        
        hittable?.Hit();
    }
    
    public void Hit(int damage = 1)
    {
        this.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody2D>();
    }
}
