using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MofuBulletRing : MonoBehaviour
{
    [SerializeField] private MofuBullet mofuBullet;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DestroyObject destroyObject;
    private void Start()
    {
        rb.velocity = transform.right * mofuBullet.Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            mofuBullet.TransformEnemy = collision.GetComponent<Transform>();
            mofuBullet.Speed = mofuBullet.Speed * 2;
            destroyObject.DestroyGameObject();
        }
    }

    private void Update()
    {
        if(mofuBullet == null)
        {
            destroyObject.DestroyGameObject();
        }
    }
}
