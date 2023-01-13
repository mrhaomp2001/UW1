using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCurveRotate : MonoBehaviour
{
    [SerializeField] private float rotateUnit;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private Timer timerChangeRotate;
    [SerializeField] private float timeRotate;
    [SerializeField] private Timer timerChangeDirection;
    [SerializeField] private float timeDirection;
    private float direction;

    private void Start()
    {
        direction = -1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerChangeDirection.IsCompleted())
        {
            if (direction == -1)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            timerChangeDirection.SetTime(timeDirection);
        }

        if (timerChangeRotate.IsCompleted())
        {
            timerChangeRotate.SetTime(timeRotate);
            if (bulletTransform != null)
            {
                bulletTransform.Rotate(0f, 0f, (rotateUnit * direction));
            }
        }
    }
}
