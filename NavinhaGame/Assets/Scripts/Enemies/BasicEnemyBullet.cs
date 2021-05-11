using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField] private OnBecomeInvisibleSignal _invisibleSignal;

    private void Update()
    {
        _rigidbody.velocity = this.transform.right * 5;
    }
    
    private void OnEnable()
    {
        _invisibleSignal.OnInvisibleBecame += ListenBecameInvisible;
    }

    private void OnDisable()
    {
        _invisibleSignal.OnInvisibleBecame -= ListenBecameInvisible;
    }
    
    private void ListenBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hittable = other.gameObject.GetComponent<IHittable>();
        
        hittable?.Hit();
        
        this.gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody2D>();
        
        if (_invisibleSignal) return;
        _invisibleSignal = this.GetComponentInChildren<OnBecomeInvisibleSignal>();

        if (_invisibleSignal) return;
        _invisibleSignal = this.GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<OnBecomeInvisibleSignal>();
    }
}
