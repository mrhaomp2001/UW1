using UnityEngine;

public class StormTriggerC1S1 : MonoBehaviour
{
    [SerializeField] private GameObject stormObject;

    [SerializeField] private GameObject finalEnemy;

    [SerializeField] private Timer timer;

    private bool isTrigged;

    private bool isStormEnded;

    private void Update()
    {
        if (timer.timeTotal <= 0 || finalEnemy == null)
        {
            isStormEnded = true;
        }

        if (isStormEnded)
        {
            stormObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigged)
        {
            if (collision.tag == "Player")
            {
                StartStorm();
                timer.StartTimer();
                isTrigged = true;
            }
        }
    }

    public void StartStorm()
    {
        stormObject.SetActive(true);
    }

    public void EndStorm()
    {
        isStormEnded = true;
    }
}
