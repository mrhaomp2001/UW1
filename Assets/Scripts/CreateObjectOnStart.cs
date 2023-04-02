using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectOnStart : MonoBehaviour
{
    [SerializeField] private GameObject createObject;
    private void Start()
    {
        Instantiate(createObject, transform.position, transform.rotation);
    }
}
