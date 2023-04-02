using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    [SerializeField] private List<TriggerContent> triggerContents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if(collision.tag == triggerContent.tagTrigger)
            {
                triggerContent.enterEvent.Invoke();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if (collision.tag == triggerContent.tagTrigger)
            {
                triggerContent.stayEvent.Invoke();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var triggerContent in triggerContents)
        {
            if (collision.tag == triggerContent.tagTrigger)
            {
                triggerContent.exitEvent.Invoke();
            }
        }
    }
}
