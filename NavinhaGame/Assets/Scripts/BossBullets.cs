using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets : MonoBehaviour
{
    public float speed;
    public Transform gun;
    Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rbody.velocity = gun.transform.right * -speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var hit = col.gameObject.GetComponent<IHittable>();
        hit?.Hit(1);
        if (col.gameObject)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
