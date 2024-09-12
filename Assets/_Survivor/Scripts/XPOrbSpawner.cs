using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class XPOrbSpawner : MonoBehaviour
{
    [SerializeField]
    Health _health;

    [SerializeField]
    XPDrop _dropPrefab;


    // Start is called before the first frame update
    void Awake()
    {
        if (_health is null)
        {
            _health = GetComponentInParent<Health>();
        }
        Assert.IsNotNull(_health);
        Assert.IsNotNull(_dropPrefab);
        _health.Died.AddListener(() =>
        {
            //Debug.Log("Trying to Instantiate xp orb drop");
            var drop = Instantiate(_dropPrefab);
            drop.transform.position = transform.parent.transform.position;
        });
    }
}
