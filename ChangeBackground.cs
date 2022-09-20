using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int State;

    public void Change()
    {
        animator.SetInteger("State", State);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Change();
        }
    }
}
