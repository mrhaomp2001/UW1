using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private bool isDestroyByTime;
    [SerializeField] private float destroyTime;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private Color destroyEffectColor;

    public void Start()
    {
        DestroyByTime();
    }

    public void DestroyGameObject()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation).GetComponent<SpriteRenderer>().color = destroyEffectColor;
        }
        Destroy(this.gameObject);
    }

    private void DestroyByTime()
    {
        if (isDestroyByTime)
        {
            StartCoroutine(DestroyThisGameObjectByTime());
        }
    }

    IEnumerator DestroyThisGameObjectByTime()
    {
        yield return new WaitForSeconds(destroyTime);
        DestroyGameObject();
    }
}
