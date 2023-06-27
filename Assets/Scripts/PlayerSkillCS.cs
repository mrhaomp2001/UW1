using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCS : MonoBehaviour
{
    [SerializeField] private float timeInvicible;
    [SerializeField] private float timeShakeScreen;
    public void Effect()
    {
        FindObjectOfType<ShakeScreenEffect>().ShakeScreen(timeShakeScreen);
        FindObjectOfType<PlayerController>().playerInvicible(timeInvicible);
    }
}
