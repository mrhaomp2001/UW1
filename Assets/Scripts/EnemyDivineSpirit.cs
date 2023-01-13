using UnityEngine;

public class EnemyDivineSpirit : MonoBehaviour
{
    [SerializeField] private GameObject bulletGroup;
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyMove enemyMove;

    public void Fire()
    {
        if (enemyMove.isEnemyMove)
        {
            if (enemy.isObjectInPlayerArea)
            {
                Quaternion fireRot = new Quaternion(0f, 0f, 0f, 0f);
                Instantiate(bulletGroup, transform.position, fireRot);
            }
        }
    }
}
