using UnityEngine;

public class BasicEnemy : AEnemy
{
    [SerializeField] private float _moveSpeed = 5f;
    
    protected override void Move()
    {
        this._rigidbody.velocity = this.transform.right * _moveSpeed;
    }
}