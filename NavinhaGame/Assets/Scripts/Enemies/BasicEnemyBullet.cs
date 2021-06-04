using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [FormerlySerializedAs("_invisibleSignal")] [SerializeField] private VisibleInvisibleSignals invisibleSignals;

    private void Update()
    {
        _rigidbody.velocity = this.transform.right * 5;
    }
    
    private void OnEnable()
    {
        invisibleSignals.OnBecameInvisibleSignal += ListenBecameInvisible;
    }

    private void OnDisable()
    {
        invisibleSignals.OnBecameInvisibleSignal -= ListenBecameInvisible;
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
        
        if (invisibleSignals) return;
        invisibleSignals = this.GetComponentInChildren<VisibleInvisibleSignals>();

        if (invisibleSignals) return;
        invisibleSignals = this.GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<VisibleInvisibleSignals>();
    }
}
