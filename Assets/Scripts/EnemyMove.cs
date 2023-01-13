using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector] public bool isMoveLeft, isMoveDown;

    [Header(" >> Stats: ")]
    private bool isEnemyCanMove;
    public bool isEnemyMove;
    [SerializeField] private bool isEnemyJump;
    [SerializeField] private float xVelocity, yVelocity;


    [Header(" >> Component: ")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform upLeftCap, downRightCap;
    private PlayerController playerController;

    public float XVelocity { get => xVelocity; set => xVelocity = value; }
    public float YVelocity { get => yVelocity; set => yVelocity = value; }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        if (isEnemyMove)
        {
            if (isEnemyCanMove)
            {
                Move();
            }
            else
            {
                rb.velocity = new Vector3(0, 0);
            }
        }
    }

    private void Move()
    {
        if (!isEnemyJump)
        {
            if (transform.position.y <= downRightCap.position.y)
            {
                isMoveDown = false;
            }
            else if (transform.position.y >= upLeftCap.position.y)
            {
                isMoveDown = true;
            }

            if (isMoveDown)
            {
                rb.velocity = new Vector3(rb.velocity.x, -yVelocity);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, yVelocity);
            }
        }

        if (transform.position.x <= upLeftCap.position.x)
        {
            isMoveLeft = false;
        }
        else if (transform.position.x >= downRightCap.position.x)
        {
            isMoveLeft = true;
        }

        if (isMoveLeft)
        {
            if (xVelocity > 0)
            {
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            rb.velocity = new Vector3(-xVelocity, rb.velocity.y);
        }
        else
        {
            if (xVelocity > 0)
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            rb.velocity = new Vector3(xVelocity, rb.velocity.y);
        }

        if (xVelocity == 0)
        {
            if (playerController.transform.position.x < transform.position.x)
            {
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
            if (playerController.transform.position.x > transform.position.x)
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isEnemyCanMove = true;
        }
    }

    public void EnemyJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, yVelocity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isEnemyCanMove = false;
        }
    }
}
