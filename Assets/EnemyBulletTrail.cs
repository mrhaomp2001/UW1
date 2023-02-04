using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTrail : MonoBehaviour
{

    [SerializeField] private Transform createTransform;
    [SerializeField] private List<GameObject> bullets;
    private float runtime;
    [SerializeField] private float timeRate;
    [SerializeField] private bool isUseOriginRotation;

    private void Start()
    {
        runtime = Time.time;
        // runtime += timeRate;
    }
    private void FixedUpdate()
    {
        if (runtime <= Time.time && createTransform != null)
        {
            if (!isUseOriginRotation)
            {
                Instantiate(bullets[Random.Range(0, bullets.Count)], createTransform.position, createTransform.rotation);
            }
            else
            {
                Instantiate(bullets[Random.Range(0, bullets.Count)], createTransform.position, new Quaternion(0f, 0f, 0f, 0f));

            }
            runtime += timeRate;
        }
    }
}
