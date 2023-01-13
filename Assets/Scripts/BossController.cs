using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Transform bossSpriteTransform;

    [SerializeField] private Boss boss;

    [SerializeField] private DialogueTrigger BossEndDialogue;
        
    public void setState(int state)
    {
        anim.SetInteger("State", state);
    }

    public void TriggerBossEndDialogue()
    {
        BossEndDialogue.TriggerDialogue();
    }

    public void RotateYBossSprite(float angle)
    {
        bossSpriteTransform.rotation = new Quaternion(0f, angle, 0f, 0f);
    }
}
