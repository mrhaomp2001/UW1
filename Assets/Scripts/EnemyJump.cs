using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [SerializeField] private Transform transformEnemy;

    private void Update()
    {
        transform.position = transformEnemy.position;
        transform.rotation = transformEnemy.rotation;
    }
}
