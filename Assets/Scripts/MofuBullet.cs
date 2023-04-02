using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MofuBullet : MonoBehaviour
{
    [SerializeField] private Transform transformEnemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private bool isSelectedEnemyTransform;
    Vector2 moveDirection;

    public Transform TransformEnemy { get => transformEnemy; set => transformEnemy = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Update()
    {
        if (transformEnemy != null && isSelectedEnemyTransform == false)
        {
            moveDirection = (transformEnemy.position - transform.position).normalized * Speed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

            rb.rotation = -Mathf.Atan2((transformEnemy.position - transform.position).x, (transformEnemy.position - transform.position).y) * Mathf.Rad2Deg + 90f;

            isSelectedEnemyTransform = true;
        }
        else
        {
            rb.velocity = transform.right * Speed;
        }
    }
}
