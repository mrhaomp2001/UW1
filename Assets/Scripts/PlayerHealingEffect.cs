using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealingEffect : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = FindObjectOfType<PlayerController>().GetComponent<Transform>().position;
    }
}
