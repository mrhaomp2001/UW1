using UnityEngine;

public class EnemyBee : MonoBehaviour
{
    public EnemyMoveToPlayer enemyMoveToPlayer;

    public void ChangeMoveTrue()
    {
        enemyMoveToPlayer.ChangeMove(true);
    }

    public void ChangeMoveFalse()
    {
        enemyMoveToPlayer.ChangeMove(false);
    }
}
