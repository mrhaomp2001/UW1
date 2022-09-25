using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Boss boss;
        
    public void setState(int state)
    {
        anim.SetInteger("State", state);
    }
}
