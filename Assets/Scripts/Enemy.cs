using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyHp;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject ObjectContainer;
    [SerializeField] protected GameObject dieAnimation;
    [SerializeField] protected Color dieAnimationColor;

    public bool isObjectInPlayerArea;


    public void TakeDmg()
    {
        animator.SetTrigger("Hurt");
    }

    public void EnemyDie()
    {
        if (enemyHp <= 0)
        {
            Instantiate(dieAnimation, transform.position, transform.rotation).GetComponent<SpriteRenderer>().color = dieAnimationColor;
            Destroy(ObjectContainer);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            TakeDmg();
            enemyHp -= (collision.GetComponent<PlayerBullet>().Damage + FindObjectOfType<PlayerController>().Damage);
        }

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Hurt();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isObjectInPlayerArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            isObjectInPlayerArea = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt();
        }
    }

    private void Update()
    {
        EnemyDie();
    }
}
