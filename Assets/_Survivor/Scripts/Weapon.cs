using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet _bulletPrefab;

    List<Bullet> _pooledBullets = new List<Bullet>();

    List<Bullet> _activeBullets = new List<Bullet>();

    float _fireInteral = 0.5f;

    void Awake()
    {

        for (int i = 0; i < 100; ++i)
        {
            var bullet = Instantiate(_bulletPrefab);
            bullet.gameObject.SetActive(false);
            _pooledBullets.Add(bullet);
        }

    }

    float _timeSinceLastFire;

    int _enemyIndex;

    void Update()
    {
        _timeSinceLastFire += Time.deltaTime;

        if (_timeSinceLastFire > _fireInteral)
        {
            _timeSinceLastFire = 0;

            var currentPosition = transform.position;

            _enemyIndex += 1;

            //foreach (var mob in Mob.Actives)
            if (Mob.Actives.Count != 0)
            {
                Mob mob = Mob.Actives[_enemyIndex % Mob.Actives.Count];

                if (_pooledBullets.Count == 0)
                    return;

                var instance = _pooledBullets[_pooledBullets.Count - 1];
                _pooledBullets.RemoveAt(_pooledBullets.Count - 1);

                /*if(instance.transform.position != Vector3.zero)
                {
                    Debug.Log("Reused a bullet " + name, instance);
                }*/

                instance.gameObject.SetActive(true);
                instance.ResetState();

                Vector3 direction = (mob.transform.position - currentPosition);
                direction.y = 0;
                direction.Normalize();

                instance.transform.rotation = Quaternion.LookRotation(direction);
                instance.transform.position = currentPosition + direction;
                //instance.transform.position += direction;

                _activeBullets.Add(instance);
            }
        }

        foreach (var bullet in _activeBullets)
        {
            bullet.UpdateState();
        }


        List<Bullet> temp = new List<Bullet>();

        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            if (_activeBullets[i].IsDone)
            {
                var bullet = _activeBullets[i];
                //bullet.ResetState();dddddd
                _pooledBullets.Add(bullet);
                //temp.Add(bullet);
                bullet.gameObject.SetActive(false);
                _activeBullets.RemoveAt(i);
            }
        }
        //_activeBullets.RemoveAll(b => b.IsDone);
        //_activeBullets.RemoveAll(b => temp.Contains(b));
    }
}
