using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] private Text resolutionLabel;

    public AudioMixer audioMixer;
    private float currentVolume;

    private void Start()
    {
        resolutions = Screen.resolutions.ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].ToString();
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        SetResolution(currentResolutionIndex);

        resolutionLabel.text = resolutions[currentResolutionIndex].ToString();
    }

    private void Update()
    {
        resolutionLabel.text = Screen.currentResolution.ToString();
        audioMixer.GetFloat("volume", out currentVolume);
        if(currentVolume <= -20)
        {
            SetVolume(-80f);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void QuitToStartScene()
    {
        Transition transition = FindObjectOfType<Transition>();
        transition.SceneName = "StartMenu";
        transition.X = 0f;
        transition.Y = 0f;
        transition.StartAnimation(1);
    }
}
