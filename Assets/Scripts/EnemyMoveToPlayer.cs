using UnityEngine;

public class EnemyMoveToPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;

    [SerializeField] private bool isEnemyMove;

    private bool isEnemyCanMove;

    private bool isMoving;
    private PlayerController playerController;
    private Vector2 moveDirection;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (isEnemyMove)
        {
            if (rb.velocity.x < 0)
            {
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            else if (rb.velocity.x > 0)
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }

            if (isEnemyCanMove)
            {
                Move();
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isEnemyCanMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isEnemyCanMove = false;
        }
    }

    public void ChangeMove(bool isAllowMove)
    {
        if (isEnemyCanMove)
        {
            isMoving = isAllowMove;
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            moveDirection = (playerController.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void MoveUp(float speedUp)
    {
        rb.velocity = new Vector2(moveDirection.x, speedUp);
    }
}
