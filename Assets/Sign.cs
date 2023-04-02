using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    [SerializeField] private Canvas canvasWorldSpace;

    private void Update()
    {
        if(canvasWorldSpace.worldCamera == null)
        {
            canvasWorldSpace.worldCamera = FindObjectOfType<MainCamera>().GetComponent<Camera>();
        }
    }
}
