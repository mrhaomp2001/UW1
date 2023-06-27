using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FioaHealing : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPlayerHealingEffect;
    public void Healling()
    {
        GameData.PLAYER_HP++;
        Instantiate(gameObjectPlayerHealingEffect, transform.position, new Quaternion());
    }
}
