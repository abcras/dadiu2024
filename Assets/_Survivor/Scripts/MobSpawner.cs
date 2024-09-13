using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    float spawnInterval;

    private float currentTime;

    [SerializeField]
    List<Mob> _Prefabs;

    private void Start()
    {
        //TODO get all mob prefabs here in code instead of manually assigning them!
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;


        if (currentTime > spawnInterval)
        {
            currentTime = 0;
            //Debug.Log("Trying to Instantiate xp orb drop");

            if (_Prefabs.Count > 0)
            {
                var index = Random.Range(0, _Prefabs.Count);

                var mob = Instantiate(_Prefabs[index]);
                mob.transform.position = transform.position;
            }
        }
    }
}
