using UnityEngine;

public class EnemyFireBullet : MonoBehaviour
{
    [SerializeField] private bool isControlled;
    [SerializeField] private Enemy enemy;
    [SerializeField] private BulletGroupSpawner bulletGroupSpawner;
    [SerializeField] private Timer timer;
    [SerializeField] private float delayFireAverage;

    [SerializeField] private bool isUseBulletGroupRotation;

    [Header(" >> Random Delay: ")]
    [SerializeField] private bool isRandomFire;
    [SerializeField] private float minTimeFire;
    [SerializeField] private float maxTimeFire;

    private void Update()
    {
        if (isControlled)
        {
            if (enemy.isObjectInPlayerArea)
            {
                if (timer.IsCompleted())
                {
                    for (int i = 0; i < bulletGroupSpawner.BulletGroups.Count; i++)
                    {
                        if (isUseBulletGroupRotation)
                        {
                            Instantiate(bulletGroupSpawner.BulletGroups[i].BulletGroup, bulletGroupSpawner.BulletGroups[i].CreatePosition.position, bulletGroupSpawner.BulletGroups[i].CreatePosition.rotation);
                        }
                        else
                        {
                            Instantiate(bulletGroupSpawner.BulletGroups[i].BulletGroup, bulletGroupSpawner.BulletGroups[i].CreatePosition.position, new Quaternion(0f, 0f, 0f, 0f));
                        }
                    }
                    if (isRandomFire)
                    {
                        delayFireAverage = Random.Range(minTimeFire, maxTimeFire);
                    }
                    timer.SetTime(delayFireAverage);
                }
            }
        }
    }
}
