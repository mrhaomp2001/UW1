using UnityEngine;

public class PlayerJumpCheck : MonoBehaviour
{
    public bool isJumpAble;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Ground")
        {
            isJumpAble = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Ground")
        {
            isJumpAble = false;
        }
    }

    public bool IsCanJump()
    {
        return isJumpAble;
    }
}
