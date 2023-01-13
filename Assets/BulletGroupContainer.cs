using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGroupContainer : MonoBehaviour
{
    private void Start()
    {

    }

    public void ResetRotion()
    {
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
