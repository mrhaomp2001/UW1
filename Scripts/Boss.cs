using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private List<int> hp;
    [SerializeField] private int currentState;
    [SerializeField] private int stateCount;
    [SerializeField] private float bossHp;
    public bool isStarted;

    [SerializeField] private BossController bossController;
    [SerializeField] private GameObject destroyAllBullet;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            bossHp -= (collision.GetComponent<PlayerBullet>().Damage + FindObjectOfType<PlayerController>().Damage);
        }
    }

    private void Update()
    {
        if (isStarted)
        {
            if (bossHp <= 0)
            {
                if (currentState < stateCount)
                {
                    currentState++;
                    bossController.setState(currentState);
                    bossHp = hp[currentState - 1];
                    Instantiate(destroyAllBullet, transform.position, transform.rotation);
                }
                else
                {
                    currentState++;
                    bossController.setState(currentState);
                    Instantiate(destroyAllBullet, transform.position, transform.rotation);
                }
            }
        }
    }
}
