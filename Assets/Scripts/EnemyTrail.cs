using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrail : MonoBehaviour
{
    private Transform transformPlayer;
    [SerializeField] private Transform tranformCreate;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject trail;
    [SerializeField] private float spawnRate;

    private void Update()
    {
        if (transformPlayer == null)
        {
            transformPlayer = FindObjectOfType<PlayerController>().transform;
        }
        if (transformPlayer != null)
        {
            this.transform.position = transformPlayer.position;
            if(timer.IsCompleted())
            {
                Instantiate(trail, tranformCreate.position, new Quaternion(0f, 0f, 0f, 0f));
                timer.SetTime(spawnRate);
            }
        }
    }
}
