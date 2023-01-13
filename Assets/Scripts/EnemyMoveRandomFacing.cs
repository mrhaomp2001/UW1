using UnityEngine;

public class EnemyMoveRandomFacing : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private int facingChangeRate;
    [SerializeField] private float timeRate;

    [SerializeField] private EnemyMove enemyMove;

    private void Update()
    {
        if (timer.IsCompleted()) 
        {
            if (Random.Range((int)0, facingChangeRate + 1) == 0)
            {
                enemyMove.isMoveLeft = !enemyMove.isMoveLeft;
            }
            timer.SetTime(timeRate);
        }
    }
}
