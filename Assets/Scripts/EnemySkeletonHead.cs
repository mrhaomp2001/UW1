using UnityEngine;

public class EnemySkeletonHead : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private Timer timer;

    [SerializeField] private float fireRate;

    [SerializeField] private GameObject bulletGroup;

    private void Update()
    {
        if (enemyMove.isEnemyMove)
        {
            if (enemy.isObjectInPlayerArea)
            {
                if (timer.timeTotal <= 0)
                {
                    Instantiate(bulletGroup, transform.position, new Quaternion(0f, 0f, 0f, 0f));
                    timer.SetTime(fireRate);
                }
            }
        }
    }
}
