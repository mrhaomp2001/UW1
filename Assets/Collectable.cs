using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private DestroyObject destroyObject;

    Vector2 moveDirection;

    public Transform TransformPlayer { get => transformPlayer; set => transformPlayer = value; }
    public float Speed { get => speed; set => speed = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameData.PLAYER_EP += 2;
            destroyObject.DestroyGameObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArea"))
        {
            transformPlayer = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        }
    }

    private void Update()
    {
        if (transformPlayer != null)
        {
            moveDirection = (transformPlayer.position - transform.position).normalized * Speed * 4;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

            rb.rotation = -Mathf.Atan2((transformPlayer.position - transform.position).x, (transformPlayer.position - transform.position).y) * Mathf.Rad2Deg + 90f;
        }
    }
}
