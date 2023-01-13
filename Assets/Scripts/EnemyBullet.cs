using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header(" >> Stats: ")]
    [SerializeField] private float speed;
    private PlayerController playerController;

    [Header(" >> Options: ")]
    [SerializeField] private bool isSpriteLookAtPlayerStart;
    [SerializeField] private bool isSpriteLookAtPlayerUpdate;
    [SerializeField] private bool isMoveToPlayerStart;
    [SerializeField] private bool isMoveToPlayerUpdate;
    [SerializeField] private bool isMoveRouteCurve;


    [Header(" >> Components: ")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DestroyObject destroyObject;

    Vector2 moveDirection;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (isMoveToPlayerStart)
        {
            BulletMove();
        }

        if (isSpriteLookAtPlayerStart)
        {
            rb.rotation = -Mathf.Atan2((playerController.transform.position - transform.position).x, (playerController.transform.position - transform.position).y) * Mathf.Rad2Deg + 90f;
        }
        else if(isMoveRouteCurve)
        {
           
        }
    }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (isMoveToPlayerUpdate)
        {
            BulletMove();
        }
        else if(isMoveRouteCurve)
        {

        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            destroyObject.DestroyGameObject();
            collision.GetComponent<PlayerController>().Hurt();
        }
    }

    public void BulletMove()
    {
        moveDirection = (playerController.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        if (isSpriteLookAtPlayerUpdate)
        {
            rb.rotation = -Mathf.Atan2((playerController.transform.position - transform.position).x, (playerController.transform.position - transform.position).y) * Mathf.Rad2Deg + 90f;
        }
    }
}
