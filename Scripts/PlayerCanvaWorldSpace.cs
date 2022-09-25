using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvaWorldSpace : MonoBehaviour
{
    [SerializeField] private Transform playerTranform;

    private void Update()
    {
        transform.position = playerTranform.position;
    }
}
