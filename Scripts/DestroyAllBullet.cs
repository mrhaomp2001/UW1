using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllBullet : MonoBehaviour
{
    private GameObject[] bullets;
    private GameObject[] bulletGroups;
    [SerializeField] private Timer timer;

    private void Awake()
    {
        DestroyAllBullets();
    }
    private void Start()
    {
        DestroyAllBullets();
    }

    private void Update()
    {
        if(timer.IsCompleted())
        {
            DestroyAllBullets();
            timer.SetTime(0.2f);
        }
    }

    private void DestroyAllBullets()
    {
        bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<DestroyObject>().DestroyGameObject();
        }

        bulletGroups = GameObject.FindGameObjectsWithTag("BulletSpawner");
        foreach (GameObject bulletGroup in bulletGroups)
        {
            Destroy(bulletGroup);
        }
    }
}
