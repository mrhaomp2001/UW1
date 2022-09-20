using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Timer timer;
    [SerializeField] private float fireRate;

    private void Update()
    {
        if (timer.IsCompleted())
        {
            timer.SetTime(fireRate);
            Instantiate(Bullet, transform.position, transform.rotation);
        }
    }
}
