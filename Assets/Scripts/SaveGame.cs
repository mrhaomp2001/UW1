using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private int lv;
    private void Start()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController.Lv < lv)
        {
            playerController.Lv = lv;
            playerController.SaveGame();
        }
    }
}
