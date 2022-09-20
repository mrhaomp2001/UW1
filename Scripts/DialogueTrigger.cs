using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<Animator> animatorsChange;
    public List<int> animatorsState;

    public DialogueTrigger nextDialogue;

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, nextDialogue);
        for (int i = 0; i < animatorsChange.Count; i++)
        {
            animatorsChange[i].SetInteger("State", animatorsState[i]);
        }
    }
}
