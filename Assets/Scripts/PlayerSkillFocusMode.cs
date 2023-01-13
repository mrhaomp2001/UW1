using UnityEngine;

public class PlayerSkillFocusMode : MonoBehaviour
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
}
