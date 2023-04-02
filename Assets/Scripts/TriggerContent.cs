using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerContent
{
    public string tagTrigger;
    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;
}
