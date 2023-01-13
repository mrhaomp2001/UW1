using UnityEngine;
using UnityEngine.U2D;


public class MainCamera : MonoBehaviour
{
    public PixelPerfectCamera pixelPerfectCamera;

    private void Update()
    {
        ChangeRefResolutions();
    }

    public void ChangeRefResolutions()
    {
        pixelPerfectCamera.refResolutionX = Screen.width;
        pixelPerfectCamera.refResolutionY = Screen.height;
    }
}
