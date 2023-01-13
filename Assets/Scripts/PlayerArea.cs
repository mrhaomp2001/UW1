using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private void Update()
    {
        transform.position = playerTransform.position;
    }
}
