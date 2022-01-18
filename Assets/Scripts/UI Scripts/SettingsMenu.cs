using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private bool found = false;

    public Dropdown resolutionDropdown;
    public Dropdown languageDropdown;

    private Resolution[] resolutions;
    List<string> languages;

    public void Start()
    {
        languages = new List<string>();
        languages.Add("Arabic");
        languages.Add("Chinese");
        languages.Add("English");
        languages.Add("French");
        languages.Add("German");
        languages.Add("Italian");
        languages.Add("Japanese");
        languages.Add("Korean");
        languages.Add("Portuguese");
        languages.Add("Russian");
        languages.Add("Spanish");

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            found = false;
            foreach (var item in options)
            {
                if (item == option)
                    found = true;
            }
            if (!found)
                options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        languageDropdown.value = 2;
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetLanguage(int languageIndex)
    {
        string language = languages[languageIndex];
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(language);
    }
}
