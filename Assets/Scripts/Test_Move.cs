using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Test_Move : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger2;
    [SerializeField] private bool isTalked;

    public UnityEvent unityEvents;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isTalked)
        {
            dialogueTrigger2.TriggerDialogue();
            isTalked = true;
        }
    }

    public void printHello()
    {
        print("Hello");
    }

    // Update is called once per frame
    public void GameStart()
    {
        unityEvents.Invoke();
        SceneManager.LoadScene(1);
        FindObjectOfType<PlayerController>().GetComponent<Transform>().position = new Vector3(113f, -10f);
        GameData.PLAYER_HP = 5;
    }
}
