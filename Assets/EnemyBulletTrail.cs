using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTrail : MonoBehaviour
{

    [SerializeField] private Transform createTransform;
    [SerializeField] private List<GameObject> bullets;
    private float runtime;
    [SerializeField] private float timeRate;

    private void Start()
    {
        runtime = Time.time;
        // runtime += timeRate;
    }
    private void FixedUpdate()
    {
        if (runtime <= Time.time && createTransform != null)
        {
            Instantiate(bullets[Random.Range(0, bullets.Count)], createTransform.position, createTransform.rotation);
            runtime += timeRate;
        }
    }
}
