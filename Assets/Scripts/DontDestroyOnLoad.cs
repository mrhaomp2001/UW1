using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instanceDDoL;

    private void Awake()
    {
        if (instanceDDoL == null)
        {
            instanceDDoL = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
}
