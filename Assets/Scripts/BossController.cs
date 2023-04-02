using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Transform bossSpriteTransform;
    [SerializeField] private Animator bossSpriteAnimator;

    [SerializeField] private Boss boss;

    [Header("End Dialogue")]
    [SerializeField] private DialogueTrigger BossEndDialogue;
    [SerializeField] private UnityEvent unityEventEndDialogue;

    [Header("UI & Background")]
    [SerializeField] private Animator bossImageAnimator;
    [SerializeField] private GameObject gameObjectBackground;
        
    public void setState(int state)
    {
        anim.SetInteger("State", state);
    }

    public void setStateBossSpriteAnimator(int state)
    {
        bossSpriteAnimator.SetInteger("State", state);

    }

    public void TriggerBossEndDialogue()
    {
        unityEventEndDialogue.Invoke();
        BossEndDialogue.TriggerDialogue();
    }

    public void RotateYBossSprite(float angle)
    {
        bossSpriteTransform.rotation = new Quaternion(0f, angle, 0f, 0f);
    }

    public void TriggerSkill()
    {
        bossImageAnimator.SetInteger("State", 10);
        bossImageAnimator.SetTrigger("skill");
        gameObjectBackground.SetActive(true);
    }

    public void TriggerEndSkill()
    {
        gameObjectBackground.SetActive(false);
    }
}
