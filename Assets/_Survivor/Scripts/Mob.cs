using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
public class Mob : MonoBehaviour
{

    public static List<Mob> Actives = new List<Mob>();



    [SerializeField] MobSettings _settings;

    CharacterController _controller;

    Hero _target;

    [SerializeField] Health _health;

    public Health Health => _health;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _target = Object.FindAnyObjectByType<Hero>();

        _health.MaxHealth = _settings.MaxHealth;

        _health.Died.AddListener(() =>
        {

            Destroy(gameObject);
        });
    }

    void OnEnable()
    {
        Actives.Add(this);
    }

    void OnDisable()
    {
        Actives.Remove(this);
    }
    
    void Update()
    {
        if (_target != null)
        {
            var delta = _target.transform.position - transform.position;
            if (delta.magnitude > 0)
            {
                var motion = delta.normalized * Time.deltaTime * _settings.MoveSpeed;
                _controller.Move(motion);
            }
        }
    }
}
