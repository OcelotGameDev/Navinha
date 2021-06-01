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

    private void OnValidate()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
    }
}
