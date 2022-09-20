using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllBullet : MonoBehaviour
{
    private GameObject[] bullets;
    private GameObject[] bulletGroups;

    private void Awake()
    {
        DestroyAllBullets();
    }
    private void Start()
    {
        DestroyAllBullets();
    }

    private void DestroyAllBullets()
    {
        if (bullets == null)
        {
            bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (GameObject bullet in bullets)
            {
                bullet.GetComponent<DestroyObject>().DestroyGameObject();
            }
        }

        if (bulletGroups == null)
        {
            bulletGroups = GameObject.FindGameObjectsWithTag("BulletSpawner");
            foreach (GameObject bulletGroup in bulletGroups)
            {
                Destroy(bulletGroup);
            }
        }
    }
}
