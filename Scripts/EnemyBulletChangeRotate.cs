using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletChangeRotate : MonoBehaviour
{
    [SerializeField] private Transform transformBullet;

    [SerializeField] private List<float> zRotates;
    [SerializeField] private int zRotatesIndex;
    [SerializeField] private Timer timer;
    [SerializeField] private float timerCD;

    private void Update()
    {
        if (timer.IsCompleted())
        {
            ChangeRotate();
            timer.SetTime(timerCD);
        }
    }

    private void ChangeRotate()
    {
        if (transformBullet != null)
        {
            transformBullet.Rotate(0f, 0f, zRotates[zRotatesIndex]);
        }
        if (zRotatesIndex < zRotates.Count - 1)
        {
            zRotatesIndex++;
        }
        else
        {
            zRotatesIndex = 0;
        }
    }
}
