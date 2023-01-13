using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public int BubbleChatType;

    public List<Animator> animatorsChange;

    public List<int> animatorsState;

    public DialogueTrigger nextDialogue;

    public Dialogue dialogue;

    public UnityEvent unityEvents;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueBox>().GetComponent<Animator>().SetInteger("DialogueType", BubbleChatType);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, nextDialogue);
        for (int i = 0; i < animatorsChange.Count; i++)
        {
            animatorsChange[i].SetInteger("State", animatorsState[i]);
        }
        unityEvents.Invoke();
    }
}
