using UnityEngine;

public class BackGroundSrolling : MonoBehaviour
{

    private float length, startPos;

    private Transform transformCamera;
    public float srollingEffect;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        MainCameraCinenachine mainCinemachineCamera = FindObjectOfType<MainCameraCinenachine>();
        transformCamera = mainCinemachineCamera.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (transformCamera == null)
        {
            MainCameraCinenachine mainCinemachineCamera = FindObjectOfType<MainCameraCinenachine>();
            transformCamera = mainCinemachineCamera.GetComponent<Transform>();
        }

        float temp = (transformCamera.position.x * (1 - srollingEffect));
        float dist = (transformCamera.position.x * srollingEffect);


        transform.position = new Vector3(startPos + dist, transformCamera.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
