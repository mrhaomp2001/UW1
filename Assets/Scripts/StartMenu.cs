using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Button> buttonStages;
    public void setState(int state)
    {
        animator.SetInteger("state", state);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadChapter()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        for (int i = 0; i <= playerController.Lv; i++)
        {
            buttonStages[i].interactable = true;
        }
    }

    public void SelectPartner(int number)
    {
        FindObjectOfType<PlayerController>().PlayerFocusFireType = number;
    }

    public void Difficult(int number)
    {
        GameData.GAME_DIFFICULT = number;
        GameData.PLAYER_EP = 1;

        if (number == 0)
        {
            GameData.PLAYER_HP_MAX = 17;
            GameData.PLAYER_HP = 17;
            GameData.PLAYER_MP_MAX = 17;
            GameData.PLAYER_MP = 17;
        }
        else if (number == 1)
        {
            GameData.PLAYER_HP_MAX = 5;
            GameData.PLAYER_HP = 5;
            GameData.PLAYER_MP_MAX = 5;
            GameData.PLAYER_MP = 5;
        }
    }
}
