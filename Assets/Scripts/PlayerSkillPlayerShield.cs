using UnityEngine;

public class PlayerSkillPlayerShield : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = playerTransform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            collision.GetComponent<DestroyObject>().DestroyGameObject();
        }
    }
}
