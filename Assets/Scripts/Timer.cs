using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float timeTotal;
    [SerializeField] private float timeUnit;
    [SerializeField] private UnityEvent unityEvent;

    public bool isStartTimer;

    private bool isCanCount = true;

    public void SetTime(float time)
    {
        timeTotal = time;
    }

    public void StartTimer()
    {
        isStartTimer = true;
    }

    private void FixedUpdate()
    {
        if (timeUnit <= 0)
        {
            timeUnit = 0.1f;
        }

        if (isStartTimer)
        {
            if (timeTotal > 0)
            {
                if (isCanCount)
                {
                    timeTotal -= timeUnit;
                    isCanCount = false;
                    StartCoroutine(CountTime());

                    if (timeTotal <= 0)
                    {
                        timeTotal = 0;
                    }
                }

            }

        }
        if(IsCompleted())
        {
            unityEvent.Invoke();
        }
    }

    public bool IsCompleted()
    {
        if (timeTotal <= 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(timeUnit);
        isCanCount = true;
    }

}
