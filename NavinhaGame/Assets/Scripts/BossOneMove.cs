using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMove : MonoBehaviour, IHittable
{
    public float currentHp, maxHp, rotAngle;
    private Rigidbody2D rbody;
    private bool IsDead => currentHp <= 0;

    void OnEnable()
    {
        currentHp = maxHp;
    }
    
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void Hit(int damage = 1)
    {
        currentHp -= damage;

        if (IsDead) Die();
    }

    void AddRotationToBoss()
    {

        rbody.transform.Rotate(transform.rotation.x, transform.rotation.y, rotAngle);
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AddRotationToBoss();
    }
}
