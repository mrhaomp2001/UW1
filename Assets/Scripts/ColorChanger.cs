using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color color;

    private void Start()
    {
        spriteRenderer.color = color;
    }

}
