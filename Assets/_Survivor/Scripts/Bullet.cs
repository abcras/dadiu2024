using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public event System.Action HitMob;

    public bool IsDone { get; private set; }

    [SerializeField] float _speed = 20;
    [SerializeField] float _duration = 2;
    [SerializeField] float _damage = 2.5f;


    float _elapsedTime;

    Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetState()
    {
        _elapsedTime = 0;
        IsDone = false;


        Debug.Log("My rotation was " + transform.rotation + " " + name, this);

        _rigidbody.transform.position = Vector3.zero;
        _rigidbody.transform.rotation = Quaternion.identity;
        /*_rigidbody.MovePosition(Vector3.zero);
        _rigidbody.MoveRotation(Quaternion.identity);*/
        //_rigidbody.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }


    public void UpdateState()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _duration)
        {
            IsDone = true;
            //Debug.Log("A bullet expired " + name, this);
        }

       /* if( _rigidbody.position == Vector3.zero )
        {
            Debug.Log("Test");
        }*/

        _rigidbody.position = transform.position + transform.forward * _speed * Time.deltaTime;
        //_rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Mob>(out var mob))
        {
            HitMob?.Invoke();
            if (!IsDone)
                mob.Health.TakeDamage(_damage);
            IsDone = true;
        }
    }
}
