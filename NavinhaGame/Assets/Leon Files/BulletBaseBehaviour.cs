using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseBehaviour : MonoBehaviour
{
    public float speed;

    Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rbody.velocity = Vector2.right * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
