using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    [SerializeField] private Animator animatorDialogueBox;

    [SerializeField] private float textSpeed;

    private Queue<string> sentences;

    private DialogueTrigger nextDialogueTrigger;

    private bool canShowNextSentence;
    public void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger nextDialogue)
    {
        nextDialogueTrigger = nextDialogue;

        sentences.Clear();

        nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentence)
        {
            sentences.Enqueue(sentence);
        }
        canShowNextSentence = true;
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (canShowNextSentence)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            canShowNextSentence = false;
        }

        if (FindObjectOfType<PlayerController>() != null)
        {
            FindObjectOfType<PlayerController>().IsTalking = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
            if (dialogueText.text == sentence)
            {
                canShowNextSentence = true;
            }
        }
    }
    void EndDialogue()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            FindObjectOfType<PlayerController>().IsTalking = false;
        }
        if (nextDialogueTrigger != null)
        {
            nextDialogueTrigger.TriggerDialogue();
        }
        else
        {
        
        }
    }
}
