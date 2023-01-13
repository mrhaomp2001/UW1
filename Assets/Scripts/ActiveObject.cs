using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] GameObject objectNeedSetActive;

    private void Update()
    {
        if (timer.IsCompleted())
        {
            objectNeedSetActive.SetActive(true);
        }
    }
}
