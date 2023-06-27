using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider sliderBossHp;
    [SerializeField] private Text textBossHp;
    [SerializeField] private Boss boss;
    private bool isStart, isEnd;

    public bool IsStart { get => isStart; set => isStart = value; }
    public bool IsEnd { get => isEnd; set => isEnd = value; }

    private void Update()
    {
        textBossHp.text = boss.BossHp.ToString();
        sliderBossHp.value = boss.BossHp;
        if (boss.CurrentState > 0 && boss.CurrentState < boss.StateCount)
        {
            sliderBossHp.maxValue = boss.Hp[boss.CurrentState - 1];
            if (!isStart)
            {
                isStart = true;
                animator.SetInteger("state", 1);
            }
        }
        if (isEnd)
        {
            animator.SetInteger("state", 2);
        }
    }
}
