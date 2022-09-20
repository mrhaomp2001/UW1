using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected float bossHp;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            bossHp -= (collision.GetComponent<PlayerBullet>().Damage + FindObjectOfType<PlayerController>().Damage);
        }
    }
}
