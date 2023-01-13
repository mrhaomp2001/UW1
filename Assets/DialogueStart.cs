using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStart : MonoBehaviour
{
    public UnityEvent unityEvents;
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            unityEvents.Invoke();
        }
    }

    private void Update()
    {

    }
}
