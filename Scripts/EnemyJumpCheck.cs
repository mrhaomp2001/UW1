using UnityEngine;

public class EnemyJumpCheck : MonoBehaviour
{
    [SerializeField] private EnemyMove enemyMove;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            enemyMove.EnemyJump();
        }
    }
}
