using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected bool isNoClip;
    [SerializeField] protected bool isDestroyEnemyBullet;

    [SerializeField] protected Rigidbody2D rb;

    public int Damage { get => damage; set => damage = value; }

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "EnemyBullet")
        {
            if (isDestroyEnemyBullet)
            {
                collision.GetComponent<DestroyObject>().DestroyGameObject();
            }
        }

        if (collision.tag == "Ground")
        {
            if (!isNoClip)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
