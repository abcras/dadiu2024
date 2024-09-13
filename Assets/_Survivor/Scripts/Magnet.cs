using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    Transform target;

    public float Speed;

    [SerializeField]
    private SphereCollider detectionSphere;

    [SerializeField]
    private float PullRadius;

    // Start is called before the first frame update
    void Start()
    {
        detectionSphere.radius = PullRadius;
    }

    //DO enable shennaigans

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target is not null)
        {
            Vector3 direction = target.position - gameObject.transform.position;

            direction.y = 0;
            direction.Normalize();

            transform.parent.position += direction * Speed * Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hero>(out var hero))
        {
            var othersPos = hero.gameObject.transform;
            target = othersPos;
        }
    }
}
