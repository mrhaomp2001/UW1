using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTrigger : MonoBehaviour
{
    public UnityEvent unityEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            unityEvent.Invoke();
        }
    }
}
